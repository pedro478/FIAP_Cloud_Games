using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.DTOs;
using FIAP_Cloud_Games.Infra.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Text.RegularExpressions;

namespace FIAP_Cloud_Games.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        public async Task<Usuario> CriarUsuario(UsuarioCreateDTO user)
        {
            var existente = await _repo.GetByEmailAsync(user.email);
            if (existente != null)
                throw new Exception("E-mail já cadastrado");

            if (!verificaEmaill(user.email)) throw new Exception("E-mail fora do formato");

            if (!verificaSenha(user.senha)) throw new Exception("Senha fora do formato");

            var novoUsuario = new Usuario();
            novoUsuario.Nome = user.nome;
            novoUsuario.email = user.email;
            novoUsuario.tipoAcesso = user.tipoAcesso;
            novoUsuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.senha);
            return await _repo.AddAsync(novoUsuario);
        }

        public async Task<Usuario> AtualizaUsuario(Usuario user)
        {
            var existente = await _repo.GetByIdAsync(user.IdUsuario);
            if (existente == null)
                throw new Exception("Usuário não encontrado.");


            if (!verificaEmaill(user.email)) throw new Exception("E-mail fora do formato");

            if (!verificaSenha(user.SenhaHash)) throw new Exception("Senha fora do formato");

            existente.Nome = user.Nome;
            existente.email = user.email;
            existente.tipoAcesso = user.tipoAcesso;

            if (!string.IsNullOrWhiteSpace(user.SenhaHash))
                existente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.SenhaHash);

            return await _repo.UpdateAsync(existente);
        }

        public async Task<Usuario> ValidaUsuario(LoginRequestDTO request)
        {
            var user = await _repo.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Senha, user.SenhaHash))
                throw new Exception("E-mail ou senha inválidos.");

            return user;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _repo.GetByIdAsync(id) ?? throw new Exception("Usuário não encontrado");
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _repo.GetByEmailAsync(email) ?? throw new Exception("Usuário não encontrado");
        }

        public async Task<IEnumerable<Usuario>> GetTodosUsuarios()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<bool> DeletaUsuario(int id)
        {
            return await _repo.DeleteAsync(id);
        }



        private bool verificaEmaill(string email)
        {
            return email.Contains("@") && email.Contains(".") && email.IndexOf("@") < email.LastIndexOf(".");
        }


        private bool verificaSenha(string password)
        {
            if (password.Length < 8)
                return false;

            bool temLetra = false;
            bool temDigito = false;
            bool temCaracterEsp = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) temLetra = true;
                else if (char.IsDigit(c)) temDigito = true;
                else if (!char.IsLetterOrDigit(c)) temCaracterEsp = true;
            }

            return temLetra && temDigito && temCaracterEsp;
        }

    }
}
