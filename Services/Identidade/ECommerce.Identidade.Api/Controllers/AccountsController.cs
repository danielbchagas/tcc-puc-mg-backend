#define REST
//#define RABBITMQ

using EasyNetQ;
using ECommerce.Identidade.Api.Interfaces;
using ECommerce.Identidade.Api.Models;
using ECommerce.Identidade.Handlers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client.Exceptions;
using Refit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly JwtHandler _jwtHandler;
        private readonly RabbitMqOption _rabbitMQOptions;
        private readonly IClienteService _clienteService;

        public AccountsController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILogger<AccountsController> logger, 
            IOptions<RabbitMqOption> rabbitMQOptions, 
            IClienteService clienteService,
            JwtHandler jwtHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _clienteService = clienteService;
            _jwtHandler = jwtHandler;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserDto usuario)
        {
            var novoUsuario = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                PhoneNumber = usuario.Telephone
            };

            var createUserResult = await _userManager.CreateAsync(novoUsuario, usuario.Password);

            if (!createUserResult.Succeeded)
            {
                var erros = createUserResult.Errors.Select(e => e.Description);
                _logger.LogError($"Erro em: {HttpContext.Request.Path}", erros);

                return BadRequest(createUserResult.Errors.Select(e => e.Description));
            }

            try
            {
#if RABBITMQ
                // Coloca o usuário na fila
                var result = await CriarClienteRabbitMq(usuario);

                if (!result.IsValid)
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));

#elif REST
                var createClienteResult = await CriarClienteRest(usuario);

                if (!createClienteResult.IsSuccessStatusCode)
                    return BadRequest(createClienteResult.Error.Content);
#endif
            }
            catch(Exception e)
            {
                await CreateUserRollback(usuario);

                _logger.LogError(e.Message, e.InnerException);
                return BadRequest("Não foi possível efetivar o seu cadastro.");
            }

            return Ok(await _jwtHandler.GenerateNewToken(usuario.Email));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("autenticar")]
        public async Task<IActionResult> SignIn(SignInUserDto usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(await _jwtHandler.GenerateNewToken(usuario.Email));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("autenticar-conta-externa")]
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
                    
                    // prepara e envia um email para confirmação
                    await _userManager.AddToRoleAsync(user, "Viewer");
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
                return BadRequest("Autenticação externa inválida.");
            
            var token = await _jwtHandler.GenerateNewToken(user.Email);

            return Ok(new { Token = token, IsAuthSuccessful = true });
        }

        #region Registro de cliente
        private async Task CreateUserRollback(SignUpUserDto usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);
            await _userManager.DeleteAsync(identityUser);
        }

        private async Task<ValidationResult> CriarClienteRabbitMq(SignUpUserDto usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            #region Novos objetos
            var cliente = new ClienteDto 
            {
                Id = Guid.Parse(identityUser.Id),
                Nome = usuario.Name,
                Sobrenome = usuario.LastName,
                Ativo = true
            };

            var documento = new DocumentoDto 
            {
                Numero = usuario.Document,
                ClienteId = cliente.Id
            };

            var email = new EmailDto 
            {
                Endereco = usuario.Email,
                ClienteId = cliente.Id
            };

            var telefone = new TelefoneDto 
            {
                Numero = usuario.Telephone,
                ClienteId = cliente.Id
            };
            #endregion

            #region Filas
            void OnDisconnect(object source, EventArgs e)
            {
                Policy.Handle<EasyNetQException>()
                    .Or<BrokerUnreachableException>()
                    .Retry(10);
            }

            var bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus);
            bus.Advanced.Disconnected += OnDisconnect;

            var clienteResult = await bus.Rpc.RequestAsync<ClienteDto, ValidationResult>(cliente);
            var documentoResult = await bus.Rpc.RequestAsync<DocumentoDto, ValidationResult>(documento);
            var emailResult = await bus.Rpc.RequestAsync<EmailDto, ValidationResult>(email);
            var telefoneResult = await bus.Rpc.RequestAsync<TelefoneDto, ValidationResult>(telefone);
            #endregion

            #region Validações
            var result = new ValidationResult();

            if (!clienteResult.IsValid)
                result.Errors.AddRange(clienteResult.Errors);
            
            if (!documentoResult.IsValid)
                result.Errors.AddRange(documentoResult.Errors);
            
            if (!emailResult.IsValid)
                result.Errors.AddRange(emailResult.Errors);
            
            if (!telefoneResult.IsValid)
                result.Errors.AddRange(telefoneResult.Errors);
            #endregion

            if (!result.IsValid)
                await CreateUserRollback(usuario);

            return result;
        }

        private async Task<ApiResponse<string>> CriarClienteRest(SignUpUserDto usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            #region Novos objetos
            var cliente = new ClienteDto 
            {
                Id = Guid.Parse(identityUser.Id),
                Nome = usuario.Name,
                Sobrenome = usuario.LastName,
                Ativo = true
            };

            var documento = new DocumentoDto
            {
                Numero = usuario.Document,
                ClienteId = cliente.Id
            };

            var email = new EmailDto
            {
                Endereco = usuario.Email,
                ClienteId = cliente.Id
            };

            var telefone = new TelefoneDto
            {
                Numero = usuario.Telephone,
                ClienteId = cliente.Id
            };

            cliente.Documento = documento;
            cliente.Email = email;
            cliente.Telefone = telefone;
            #endregion

            var result = await _clienteService.Adicionar(cliente, (await _jwtHandler.GenerateNewToken(usuario.Email)).Token);

            if (!result.IsSuccessStatusCode)
                await CreateUserRollback(usuario);

            return result;
        }
        #endregion
    }
}
