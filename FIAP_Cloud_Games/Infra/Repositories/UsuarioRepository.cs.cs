using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FIAP_Cloud_Games.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddAsync(Usuario user)
        {
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario> UpdateAsync(Usuario user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id) ?? new Usuario();

            if (u.IdUsuario > 0) u.Jogos = await buscaListaJogosUsuario(u) ?? new List<Jogo>();

            return u;

        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var list =  await _context.Usuarios.ToListAsync();

            foreach (var u in list)
            {
                u.Jogos = await buscaListaJogosUsuario(u);
            }


            return list;
           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return false;

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<List<Jogo>> buscaListaJogosUsuario(Usuario usuario)
        {

            var jogosIds = await _context.JogosUsuarios
                .Where(j => j.IdUsuario == usuario.IdUsuario)
                .Select(j => j.IdJogo)
                .ToListAsync();

            var jogos = await _context.Jogos
                .Where(j => jogosIds.Contains(j.IdJogo))
                .AsNoTracking()
                .ToListAsync();

            return jogos;
            

        }

    }
}
