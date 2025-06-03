using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Domain.Entities.Enums;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FIAP_Cloud_Games.Infra.Middleware;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly BaseLogger<UsuariosController> _logger;

       

        public UsuariosController( IUsuarioService usuarioService,
             BaseLogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet("Listar")]
        [Authorize]
        public async  Task<IActionResult> Get() 
        {
            try
            {
                if (User.IsInRole(tipoAcesso.Administrador.ToString()))
                {

                    var listaUsuarios = await _usuarioService.GetTodosUsuarios();

                    var listaDTO = new List<UsuarioReadDTO>();

                    foreach (var u in listaUsuarios)
                    {

                        listaDTO.Add(new UsuarioReadDTO
                        {
                            nome = u.Nome,
                            email = u.email,
                            tipoAcesso = u.tipoAcesso,                           
                            jogosComprados = u.Jogos
                        }); 

                    }
                                               

                    return Ok(listaDTO);
                }
                else return Forbid();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
               
            }
            


        }

        [HttpGet("Buscar/{id}")]
        [Authorize]
        public async Task<IActionResult>Get(int id)
        {

            try
            {
                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                if (id > 0)
                {
                    var usuario = await _usuarioService.GetUsuarioById(id);

                    UsuarioReadDTO userReturn = new UsuarioReadDTO
                    {
                        email = usuario.email,
                        nome = usuario.Nome,
                        tipoAcesso = usuario.tipoAcesso,
                        jogosComprados = usuario.Jogos

                    };

                    return Ok(userReturn);

                }
                return BadRequest("Id inválido!");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
            }

           
        }


        [HttpPost("Criar")]
        [Authorize]
        public async Task<IActionResult>Post([FromBody] UsuarioCreateDTO usuario)
        {

            try
            {

                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                if (!ModelState.IsValid) return BadRequest("Dados inválidos!");

                var novoUsuario = await _usuarioService.CriarUsuario(usuario);

                if (novoUsuario == null) return BadRequest("Falha ao criar usuario!");
                else
                {
                    UsuarioReadDTO userReturn = new UsuarioReadDTO
                    {
                        email = novoUsuario.email,
                        nome = novoUsuario.Nome,
                        tipoAcesso = novoUsuario.tipoAcesso,
                        jogosComprados = novoUsuario.Jogos ?? new List<Jogo>()

                    };


                    return Ok(userReturn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("Atualizar")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UsuarioCreateDTO usuario)
        {
            try
            {
                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                if (!ModelState.IsValid) return BadRequest("Dados inválidos!");

                var u = await _usuarioService.GetUsuarioByEmail(usuario.email);

                if (u == null) return BadRequest("Usuário não cadastrado");

                u.Nome = usuario.nome;
                u.SenhaHash = usuario.senha;
                u.email = usuario.email;
                u.tipoAcesso = usuario.tipoAcesso;

                var result = await _usuarioService.AtualizaUsuario(u) ?? new Usuario();

                if (result.IdUsuario == 0) return BadRequest("Falha ao atualizar dados do usuario");


                UsuarioReadDTO userReturn = new UsuarioReadDTO
                {
                    email = result.email,
                    nome = result.Nome,
                    tipoAcesso = result.tipoAcesso,
                    jogosComprados = result.Jogos ?? new List<Jogo>()

                };


                return Ok(userReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("Deletar/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                if (userId == 0) return BadRequest("Dados inválidos!");


                var result = await _usuarioService.DeletaUsuario(userId);

                if (!result) return BadRequest("Falha ao deletar usuário!");

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);
            }



        }


    }
}
