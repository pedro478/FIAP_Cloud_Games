using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.DTOs;

namespace FIAP_Cloud_Games.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public JogoService(IJogoRepository jogoRepo, IUsuarioRepository usuarioRepo)
        {
            _jogoRepo = jogoRepo;
            _usuarioRepo = usuarioRepo;
        }

        public async Task<Jogo> CadastrarJogo(Jogo jogo)
        {
            return await _jogoRepo.AddAsync(jogo);
        }

        public async Task<Jogo> GetJogoById(int id)
        {
            return await _jogoRepo.GetByIdAsync(id) ?? throw new Exception("Jogo não encontrado.");
        }

        public async Task<IEnumerable<Jogo>> GetTodosJogos()
        {
            return await _jogoRepo.GetAllAsync();
        }


        public async Task<Jogo> ComprarJogo(ComprarJogoDTO comprarJogoDTO)
        {


            var usuario = await _usuarioRepo.GetByEmailAsync(comprarJogoDTO.EmailUsuario)
            ?? throw new Exception("Usuário não encontrado");

            var jogo = await _jogoRepo.GetByIdAsync(comprarJogoDTO.JogoID)
                ?? throw new Exception("Jogo não encontrado");

            if (!jogo.Disponivel)
                throw new Exception("Jogo indisponível para compra");

            if (await _jogoRepo.UsuarioJaPossuiJogoAsync(usuario.IdUsuario, jogo.IdJogo))
                throw new Exception("Usuário já comprou este jogo");

            await _jogoRepo.RegistrarCompraAsync(usuario.IdUsuario, jogo.IdJogo);
            return jogo;
        }

        public Task<bool> Removejogo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
