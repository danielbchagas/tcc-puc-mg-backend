#define REST

using EasyNetQ;
using ECommerce.Identidade.Api.Interfaces;
using ECommerce.Identidade.Api.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly ILogger<UsuariosController> _logger;

        private readonly JwtOptions _jwtOptions;
        private readonly RabbitMqOptions _rabbitMQOptions;

        private readonly IClienteService _clienteService;

        public UsuariosController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<UsuariosController> logger, IOptions<JwtOptions> jwtOptions, IOptions<RabbitMqOptions> rabbitMQOptions, IClienteService clienteService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _clienteService = clienteService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> Registrar(NovoUsuario usuario)
        {
            var novoUsuario = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                PhoneNumber = usuario.Telefone
            };

            var resultado = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors.Select(e => e.Description);
                _logger.LogError($"Erro em: {HttpContext.Request.Path}", erros);

                return BadRequest(resultado.Errors.Select(e => e.Description));
            }

            try
            {
#if RABBITMQ
                // Coloca o usuário na fila
                var clienteCriadoComSucesso = await CriarClienteRabbitMq(usuario);

                if (!clienteCriadoComSucesso.IsValid)
                    return BadRequest(clienteCriadoComSucesso.Errors.Select(e => e.ErrorMessage));

#elif REST
                var clienteCriadoComSucesso = await CriarClienteRest(usuario);

                if (!clienteCriadoComSucesso.IsValid)
                    return BadRequest(clienteCriadoComSucesso.Errors.Select(e => e.ErrorMessage));
#endif
            }
            catch(Exception e)
            {
                await DesfazerOperacao(usuario);

                _logger.LogError($"Erro em: {HttpContext.Request.Path}", e.InnerException?.Message);
                return BadRequest("Não foi possível efetivar o seu cadastro!");
            }

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(LoginUsuario usuario)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, isPersistent: false, lockoutOnFailure: true);

            if (!resultado.Succeeded)
                return BadRequest();

            return Ok(await GerarToken(usuario.Email));
        }

        private async Task DesfazerOperacao(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);
            await _userManager.DeleteAsync(identityUser);
        }

        private void OnDisconnect(object source, EventArgs e)
        {
            Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .Retry(10);
        }

        #region Registrar cliente
        private async Task<ValidationResult> CriarClienteRabbitMq(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            #region Novos objetos
            var cliente = new ClienteDto 
            {
                Id = Guid.Parse(identityUser.Id),
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Ativo = true
            };

            var documento = new DocumentoDto 
            {
                Numero = usuario.Documento,
                ClienteId = cliente.Id
            };

            var email = new EmailDto 
            {
                Endereco = usuario.Email,
                ClienteId = cliente.Id
            };

            var telefone = new TelefoneDto 
            {
                Numero = usuario.Telefone,
                ClienteId = cliente.Id
            };
            #endregion

            #region Filas
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

            // Falta criar a regra para excluir todos os relacionamentos
            if (!result.IsValid)
                await DesfazerOperacao(usuario);

            return result;
        }

        private async Task<ValidationResult> CriarClienteRest(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            #region Novos objetos
            var cliente = new ClienteDto 
            {
                Id = Guid.Parse(identityUser.Id),
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Ativo = true
            };

            var documento = new DocumentoDto
            {
                Numero = usuario.Documento,
                ClienteId = cliente.Id
            };

            var email = new EmailDto
            {
                Endereco = usuario.Email,
                ClienteId = cliente.Id
            };

            var telefone = new TelefoneDto
            {
                Numero = usuario.Telefone,
                ClienteId = cliente.Id
            };

            cliente.Documento = documento;
            cliente.Email = email;
            cliente.Telefone = telefone;
            #endregion

            _clienteService.AddToken((await GerarToken(usuario.Email)).Token);

            var clienteResult = await _clienteService.Adicionar(cliente);

            var result = new ValidationResult();

            if (!clienteResult.IsValid)
                result.Errors.AddRange(clienteResult.Errors);

            // Falta criar a regra para excluir todos os relacionamentos
            if (!result.IsValid)
                await DesfazerOperacao(usuario);

            return result;
        }
        #endregion

        #region JWT
        private async Task<UsuarioJwt> GerarToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            var EPOCH = ToUnixEpochDate(DateTime.UtcNow);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, EPOCH.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, EPOCH.ToString(), ClaimValueTypes.Integer64));

            roles.ToList().ForEach(r => claims.Add(new Claim(type: "role", value: r)));

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            var tokenEscrito = EscreverToken(claimsIdentity);

            return new UsuarioJwt 
            {
                Token = tokenEscrito,
                ExpiresIn = TimeSpan.FromHours(_jwtOptions.Expiration).TotalSeconds,
                TokenUsuario = new TokenUsuario 
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    ClaimsUsuario = claims.Select(c => new ClaimUsuario { Valor = c.Value, Tipo = c.Type })
                }
            };
        }

        private string EscreverToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret)), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private long ToUnixEpochDate(DateTime data) 
            => (long)Math.Round((data.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
#endregion
    }
}
