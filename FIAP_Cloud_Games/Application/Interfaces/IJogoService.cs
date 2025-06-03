using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.DTOs;

namespace FIAP_Cloud_Games.Application.Interfaces
{
    public interface IJogoService
    {
        Task<Jogo> CadastrarJogo(Jogo jogo);
        Task<Jogo> GetJogoById(int id);
        Task<IEnumerable<Jogo>> GetTodosJogos();
        Task<bool> Removejogo(int id);

        Task<Jogo> ComprarJogo(ComprarJogoDTO comprarJogoDTO);

     
    }
}
