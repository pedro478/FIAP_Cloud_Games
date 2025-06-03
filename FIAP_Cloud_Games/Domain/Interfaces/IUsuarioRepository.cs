using FIAP_Cloud_Games.Domain.Entities;

namespace FIAP_Cloud_Games.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddAsync(Usuario user);
        Task<Usuario> UpdateAsync(Usuario user);
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<bool> DeleteAsync(int id);

    }
}
