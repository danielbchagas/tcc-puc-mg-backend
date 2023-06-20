using AutoMapper;
using ECommerce.Customers.Api.Protos;
using ECommerce.Identity.Api.Constants;
using ECommerce.Identity.Api.Handler;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
using ECommerce.Identity.Api.Services.REST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ICustomerRabbitMqClient _customerRabbitMqClient;
        private readonly IMapper _mapper;
        private readonly IViaCepService _viaCepService;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILogger<AccountController> logger,
            JwtHandler jwtHandler,
            ICustomerRabbitMqClient customerRabbitMqClient,
            IMapper mapper,
            IViaCepService viaCepService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtHandler = jwtHandler;
            _customerRabbitMqClient = customerRabbitMqClient;
            _mapper = mapper;
            _viaCepService = viaCepService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var user = new IdentityUser();

            var identityResult = await _userManager.CreateAsync(
                    new IdentityUser { UserName = request.Email, Email = request.Email, PhoneNumber = request.Phone },
                    request.Password
                );

            if (!identityResult.Succeeded)
                return BadRequest(identityResult.Errors.Select(e => e.Description));

            user = await _userManager.FindByEmailAsync(request.Email);

            identityResult = await _userManager.AddToRoleAsync(user, UserRoles.CUSTOMER);

            if (!identityResult.Succeeded)
                return BadRequest(identityResult.Errors.Select(e => e.Description));

            try
            {
                var customer = await ComposeCustomer(user.Id, request);

                await _customerRabbitMqClient.CreateCustomer(customer);
            }
            catch (Exception e)
            {
                if(user != null)
                    await _userManager.DeleteAsync(user);

                _logger.LogError(e.Message, e.InnerException);
                return BadRequest(ResponseMessages.USER_NOT_CREATED);
            }

            var token = await _jwtHandler.GenerateNewToken(request.Email);

            return Ok(token);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
                return BadRequest(ResponseMessages.LOGIN_FAILED);

            var token = await _jwtHandler.GenerateNewToken(request.Email);

            return Ok(token);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoogle(ExternalAuthRequest externalAuth)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);

            if (payload == null)
                return BadRequest(ResponseMessages.EXTERNAL_LOGIN_FAILED);
            
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            
            if (user == null)
                return BadRequest(ResponseMessages.USER_NOT_FOUND);

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(token);
        }

        private async Task<CustomerRequest> ComposeCustomer(string id, SignUpUserRequest user)
        {
            var customer = _mapper.Map<CustomerRequest>(user);
            customer.Id = Guid.Parse(id);
            customer.Address = _mapper.Map<AddressRequest>(await _viaCepService.GetAddress(user.ZipCode));
            return customer;
        } 
    }
}
