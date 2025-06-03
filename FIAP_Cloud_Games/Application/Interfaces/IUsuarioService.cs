using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace FIAP_Cloud_Games.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarUsuario(UsuarioCreateDTO user);

        Task<Usuario> AtualizaUsuario(Usuario user);

        Task<Usuario> GetUsuarioById(int id);

        Task<Usuario> GetUsuarioByEmail(string email);
        Task<IEnumerable<Usuario>> GetTodosUsuarios();
        Task<bool> DeletaUsuario(int id);

        Task<Usuario> ValidaUsuario(LoginRequestDTO request);

    }
}
