#define REST
//#define RABBITMQ

using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using ECommerce.Identity.Api.Handlers;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client.Exceptions;
using Refit;

namespace ECommerce.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly JwtHandler _jwtHandler;
        private readonly RabbitMqOption _rabbitMQOptions;
        private readonly ICustomerService _customerService;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILogger<AccountController> logger, 
            IOptions<RabbitMqOption> rabbitMQOptions,
            ICustomerService customerService,
            JwtHandler jwtHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _customerService = customerService;
            _jwtHandler = jwtHandler;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpUserDto user)
        {
            var newUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
                PhoneNumber = user.Phone
            };

            var createUserResult = await _userManager.CreateAsync(newUser, user.Password);

            if (!createUserResult.Succeeded)
            {
                var errors = createUserResult.Errors.Select(e => e.Description);

                _logger.LogError($"Erro em: {HttpContext.Request.Path}", errors);
                return BadRequest(errors);
            }

            try
            {
#if RABBITMQ
                var createCustomerResult = await CreateCustomerRabbitMq(user);

                if (!createCustomerResult.IsValid)
                    return BadRequest(createCustomerResult.Errors.Select(e => e.ErrorMessage));

#elif REST
                var createCustomerResult = await CreateCustomerRest(user);

                if (!createCustomerResult.IsSuccessStatusCode)
                    return BadRequest(createCustomerResult.Error);
#endif
            }
            catch(Exception e)
            {
                await CreateUserRollback(user);

                _logger.LogError(e.Message, e.InnerException);
                return BadRequest("Não foi possível efetivar o seu cadastro.");
            }

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(new { Token = token, IsAuthSuccessful = true });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInUserDto user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
                return BadRequest();

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(new { Token = token, IsAuthSuccessful = true });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoogle(ExternalAuthDto externalAuth)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);

            if (payload == null)
                return BadRequest("Autenticação externa inválida.");
            
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new IdentityUser { Email = payload.Email, UserName = payload.Email };
                    await _userManager.CreateAsync(user);
                    
                    await _userManager.AddToRoleAsync(user, "Viewer");
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
                return BadRequest("Autenticação externa inválida.");
            
            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(new { Token = token, IsAuthSuccessful = true });
        }

        #region Customer registration
        private async Task CreateUserRollback(SignUpUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.DeleteAsync(identityUser);
        }

        private async Task<ValidationResult> CreateCustomerRabbitMq(SignUpUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var customer = new CustomerDto 
            {
                Id = Guid.Parse(identityUser.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enabled = true
            };

            var document = new DocumentDto 
            {
                Number = user.Document,
                UserId = customer.Id
            };

            var email = new EmailDto 
            {
                Address = user.Email,
                UserId = customer.Id
            };

            var phone = new PhoneDto 
            {
                Number = user.Phone,
                UserId = customer.Id
            };

            customer.Document = document;
            customer.Email = email;
            customer.Phone = phone;

            void OnDisconnect(object source, EventArgs e)
            {
                Policy.Handle<EasyNetQException>()
                    .Or<BrokerUnreachableException>()
                    .Retry(10);
            }

            var bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus);
            bus.Advanced.Disconnected += OnDisconnect;

            var customerResult = await bus.Rpc.RequestAsync<CustomerDto, ValidationResult>(customer);
            var documentResult = await bus.Rpc.RequestAsync<DocumentDto, ValidationResult>(document);
            var emailResult = await bus.Rpc.RequestAsync<EmailDto, ValidationResult>(email);
            var phoneResult = await bus.Rpc.RequestAsync<PhoneDto, ValidationResult>(phone);

            var validation = new ValidationResult();

            if (!customerResult.IsValid)
                validation.Errors.AddRange(customerResult.Errors);
            
            if (!documentResult.IsValid)
                validation.Errors.AddRange(documentResult.Errors);
            
            if (!emailResult.IsValid)
                validation.Errors.AddRange(emailResult.Errors);
            
            if (!phoneResult.IsValid)
                validation.Errors.AddRange(phoneResult.Errors);

            if (!validation.IsValid)
                await CreateUserRollback(user);

            return validation;
        }

        private async Task<IApiResponse> CreateCustomerRest(SignUpUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var customer = new CustomerDto() 
            {
                Id = Guid.Parse(identityUser.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enabled = true
            };

            var document = new DocumentDto
            {
                Number = user.Document,
                UserId = customer.Id
            };

            var email = new EmailDto
            {
                Address = user.Email,
                UserId = customer.Id
            };

            var phone = new PhoneDto
            {
                Number = user.Phone,
                UserId = customer.Id
            };

            customer.Document = document;
            customer.Email = email;
            customer.Phone = phone;

            var result = await _customerService.Create(customer, await _jwtHandler.GenerateNewToken(user.Email));

            if (!result.IsSuccessStatusCode)
                await CreateUserRollback(user);

            return result;
        }
        #endregion
    }
}
