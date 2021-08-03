using ECommerce.Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public UsuarioController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<UsuarioController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsuarioController> _logger;

        [HttpPost("novo-usuario")]
        public async Task<IActionResult> Novo(NovoUsuario usuario)
        {
            var novoUsuario = new IdentityUser
            {
                UserName = usuario.Documento,
                Email = usuario.Email,
                //EmailConfirmed = true,
                PhoneNumber = usuario.Telefone,
                //PhoneNumberConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors.Select(e => e.Description);
                _logger.LogError($"Erro em: {HttpContext.Request.Path}", erros);

                return BadRequest();
            }

            await _signInManager.SignInAsync(novoUsuario, isPersistent: false);
            return RedirectToAction("Login", new LoginUsuario { Email = novoUsuario.Email, Senha = usuario.Senha });
        }

        [HttpPost("logar")]
        public async Task<IActionResult> Login(LoginUsuario usuario)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, isPersistent: false, lockoutOnFailure: true);

            if (resultado.Succeeded)
                return Ok();

            return BadRequest();
        }
    }
}
