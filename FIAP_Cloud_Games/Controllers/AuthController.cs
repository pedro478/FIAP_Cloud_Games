using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Application.Services;
using FIAP_Cloud_Games.DTOs;
using FIAP_Cloud_Games.Infra.Data;
using FIAP_Cloud_Games.Infra.Middleware;
using Microsoft.AspNetCore.Mvc;
namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly IUsuarioService _usuarioService;
        private readonly tokenService _tokenService;
        private readonly BaseLogger<AuthController> _logger;


        public AuthController(AppDbContext context,
                              IUsuarioService usuarioService, 
                              tokenService tokenService,
                              BaseLogger<AuthController> logger)
        {
            
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _logger = logger;
        }


        [HttpPost("auth/token")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {

                var usuario = await _usuarioService.ValidaUsuario(request);

                if (usuario == null)
                    return Unauthorized("Credenciais inválidas");

                var token = _tokenService.CriaToken(usuario);

                return Ok(new { token });
            }

            catch(Exception ex)
            {
                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
            }
           
        }
    }
}
