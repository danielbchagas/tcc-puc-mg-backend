#define gRPC
//#define RABBITMQ

using EasyNetQ;
using ECommerce.Identity.Api.Constants;
using ECommerce.Identity.Api.DTOs.Request;
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
using System;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ICustomerGrpcClient _customerGrpcClient;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILogger<AccountController> logger, 
            IOptions<RabbitMqOption> rabbitMQOptions,
            JwtHandler jwtHandler,
            ICustomerGrpcClient customerGrpcClient)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _jwtHandler = jwtHandler;
            _customerGrpcClient = customerGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var identityUser = new IdentityUser();

            try
            {
                var createUserResult = await _userManager.CreateAsync(
                    new IdentityUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone}, 
                    user.Password
                );

                identityUser = await _userManager.FindByEmailAsync(user.Email);

                await _userManager.AddToRoleAsync(identityUser, UserRoles.Customer);

                if (!createUserResult.Succeeded)
                {
                    var errors = createUserResult.Errors.Select(e => e.Description);

                    _logger.LogError($"Erro em: {HttpContext.Request.Path}", errors);
                    return BadRequest(errors);
                }

#if RABBITMQ
                var createCustomerResult = await CreateCustomerRabbitMq(user);

                if (!createCustomerResult.IsValid)
                    return BadRequest(createCustomerResult.Errors.Select(e => e.ErrorMessage));

#elif gRPC
                var createCustomerResult = await _customerGrpcClient.Create(user);

                if (!createCustomerResult.Isvalid)
                    return BadRequest(createCustomerResult.Message);
#endif
            }
            catch (Exception e)
            {
                if(identityUser != null)
                    await _userManager.DeleteAsync(identityUser);

                _logger.LogError(e.Message, e.InnerException);
                return BadRequest(ResponseMessages.UserNotCreated);
            }

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(token);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
                return BadRequest();

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(token);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoogle(ExternalAuthRequest externalAuth)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);

            if (payload == null)
                return BadRequest(ResponseMessages.ExternalLoginFailed);
            
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                try
                {
                    if (user == null)
                    {
                        user = new IdentityUser { Email = payload.Email, UserName = payload.Email };
                        await _userManager.CreateAsync(user);

                        await _userManager.AddToRoleAsync(user, UserRoles.Customer);
                        
                        var createCustomerResult = await _customerGrpcClient.Create(new SignUpUserRequest
                        {
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName,
                            Email = payload.Email,
                            Phone = null,
                            Document = null
                        });

                        if (!createCustomerResult.Isvalid)
                            return BadRequest(createCustomerResult.Message);
                    }
                }
                catch (Exception e)
                {
                    if(user != null)
                        await _userManager.DeleteAsync(user);

                    _logger.LogError(e.Message, e.InnerException);
                    return BadRequest(ResponseMessages.UserNotCreated);
                }
            }

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(token);
        }

        #region Customer registration
        private async Task<ValidationResult> CreateCustomerRabbitMq(SignUpUserRequest user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var customer = new CustomerRequest 
            {
                Id = Guid.Parse(identityUser.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enabled = true
            };

            var document = new DocumentRequest 
            {
                Number = user.Document,
                UserId = customer.Id
            };

            var email = new EmailRequest 
            {
                Address = user.Email,
                UserId = customer.Id
            };

            var phone = new PhoneRequest 
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

            var customerResult = await bus.Rpc.RequestAsync<CustomerRequest, ValidationResult>(customer);
            var documentResult = await bus.Rpc.RequestAsync<DocumentRequest, ValidationResult>(document);
            var emailResult = await bus.Rpc.RequestAsync<EmailRequest, ValidationResult>(email);
            var phoneResult = await bus.Rpc.RequestAsync<PhoneRequest, ValidationResult>(phone);

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
                await _userManager.DeleteAsync(identityUser);

            return validation;
        }
        #endregion
    }
}
