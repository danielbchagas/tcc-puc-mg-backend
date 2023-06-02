#define gRPC

using AutoMapper;
using ECommerce.Identity.Api.Constants;
using ECommerce.Identity.Api.Handler;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
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
        private readonly ICustomerGrpcClient _customerGrpcClient;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILogger<AccountController> logger,
            JwtHandler jwtHandler,
            ICustomerGrpcClient customerGrpcClient,
            ICustomerRabbitMqClient customerRabbitMqClient,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtHandler = jwtHandler;
            _customerGrpcClient = customerGrpcClient;
            _mapper = mapper;
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
                var identityResult = await _userManager.CreateAsync(
                    new IdentityUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone}, 
                    user.Password
                );

                if (!identityResult.Succeeded)
                {
                    var errors = identityResult.Errors.Select(e => e.Description);

                    _logger.LogError($"Erro em: {HttpContext.Request.Path}", errors);
                    return BadRequest(errors);
                }

                identityUser = await _userManager.FindByEmailAsync(user.Email);

                await _userManager.AddToRoleAsync(identityUser, UserRoles.Customer);

                user.Id = Guid.Parse(identityUser.Id);

#if RABBITMQ
                // Use ICustomerRabbitMqClient
#elif gRPC
                var createCustomerResult = await _customerGrpcClient.Create(_mapper.Map<Customers.Api.Protos.CreateUserRequest>(user));

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
                return BadRequest(ResponseMessages.LoginFailed);

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
                return BadRequest(ResponseMessages.UserNotFound);

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(token);
        }
    }
}
