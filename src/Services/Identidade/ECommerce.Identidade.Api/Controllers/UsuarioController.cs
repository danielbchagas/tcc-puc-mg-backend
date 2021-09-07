#define REST
//#define RABBITMQ

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
    public class UsuarioController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        private readonly ILogger<UsuarioController> _logger;

        private IBus _bus;

        private readonly JwtOptions _jwtOptions;
        private readonly RabbitMqOptions _rabbitMQOptions;

        private readonly IClienteService _clienteService;

        public UsuarioController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<UsuarioController> logger, IOptions<JwtOptions> jwtOptions, IOptions<RabbitMqOptions> rabbitMQOptions, IClienteService clienteService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _clienteService = clienteService;
        }

        [HttpPost("novo")]
        public async Task<IActionResult> Novo(NovoUsuario usuario)
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

                if (!clienteCriadoComSucesso.Ok)
                    return BadRequest(clienteCriadoComSucesso.Errors.Select(e => e));
#endif

                // Autentica o usuário
                await _signInManager.SignInAsync(novoUsuario, isPersistent: false);
            }
            catch(Exception e)
            {
                await DesfazerOperacao(usuario);

                _logger.LogError($"Erro em: {HttpContext.Request.Path}", e.InnerException?.Message);
                return BadRequest("Não foi possível efetivar o seu cadastro!");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuario usuario)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, isPersistent: false, lockoutOnFailure: true);

            if (!resultado.Succeeded)
                return BadRequest();

            return Ok(await GerarToken(usuario.Email));
        }

        private async Task<ValidationResult> CriarClienteRabbitMq(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            var cliente = new ClienteDto(id: Guid.Parse(identityUser.Id),
                nome: usuario.Nome,
                sobrenome: usuario.Sobrenome,
                documento: usuario.Documento,
                telefone: usuario.Telefone,
                email: usuario.Email
            );

            using (_bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus))
            {
                var resultado = await _bus.Rpc.RequestAsync<ClienteDto, ValidationResult>(cliente);

                if (!resultado.IsValid)
                    await _userManager.DeleteAsync(identityUser);

                return resultado;
            }
        }

        private async Task<ClienteResponseMessage> CriarClienteRest(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);

            var cliente = new ClienteDto(id: Guid.Parse(identityUser.Id),
                nome: usuario.Nome,
                sobrenome: usuario.Sobrenome,
                documento: usuario.Documento,
                telefone: usuario.Telefone,
                email: usuario.Email
            );

            var token = await GerarToken(usuario.Email);

            _clienteService.AddToken(token.Token);
            var resultado = await _clienteService.Novo(cliente);

            if (!resultado.Ok)
                await _userManager.DeleteAsync(identityUser);

            return resultado;
        }

        private async Task DesfazerOperacao(NovoUsuario usuario)
        {
            var identityUser = await _userManager.FindByEmailAsync(usuario.Email);
            await _userManager.DeleteAsync(identityUser);
        }

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
