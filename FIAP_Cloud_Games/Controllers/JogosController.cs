using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Entities.Enums;
using FIAP_Cloud_Games.DTOs;
using FIAP_Cloud_Games.Infra.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _service;
        private readonly BaseLogger<JogosController> _logger;

        public JogosController(IJogoService service, BaseLogger<JogosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        
        


        [HttpGet("Listar")]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _service.GetTodosJogos());
        }

        [HttpGet("Buscar/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var jogo = await _service.GetJogoById(id);
                return Ok(jogo);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);

            }
            
        }


        [HttpPost("Criar")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Jogo jogo)
        {
            try
            {
                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                var novo = await _service.CadastrarJogo(jogo);
                return Ok(novo);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);

            }
           
        }


        [HttpPut("Atualizar")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] editarJogoDTO jogo)
        {
            try
            {
                var _jogo = await _service.GetJogoById(jogo.id);

                _jogo.Nome = jogo.Nome;
                _jogo.Descricao = jogo.Descricao;
                _jogo.Disponivel = jogo.Disponivel;

                return Ok(jogo);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);

            }

        }


        [HttpDelete("Deletar/{id}")]
        [Authorize]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                if (!User.IsInRole(tipoAcesso.Administrador.ToString())) return Forbid();

                var ok = await _service.Removejogo(id);
                return ok ? NoContent() : NotFound();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);

            }
            
        }

        [HttpPost("comprar")]
        [Authorize]
        public async Task<IActionResult> Comprar([FromBody] ComprarJogoDTO comprar)
        {
            try
            {
                var jogo = await _service.ComprarJogo(comprar);
                return Ok(jogo);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao processar request {ex}");
                return BadRequest(ex.Message);

            }
        }


    }
}
