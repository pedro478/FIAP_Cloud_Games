using FIAP_Cloud_Games.Domain.Entities;

namespace FIAP_Cloud_Games.Domain.Interfaces
{
    public interface IJogoRepository
    {
        Task<Jogo> AddAsync(Jogo jogo);
        Task<Jogo?> GetByIdAsync(int id);
        Task<IEnumerable<Jogo>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<bool> RegistrarCompraAsync(int idUsuario, int idJogo);
        Task<bool> UsuarioJaPossuiJogoAsync(int idUsuario, int idJogo);

        Task<Jogo> AtualizaJogo(Jogo jogo);

    }
}
