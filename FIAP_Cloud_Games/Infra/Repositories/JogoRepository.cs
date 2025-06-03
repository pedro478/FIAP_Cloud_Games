using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAP_Cloud_Games.Infra.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;

        public JogoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Jogo> AddAsync(Jogo jogo)
        {
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            return jogo;
        }

        public async Task<Jogo?> GetByIdAsync(int id)
        {
            return await _context.Jogos.FindAsync(id);
        }

        public async Task<IEnumerable<Jogo>> GetAllAsync()
        {
            return await _context.Jogos.ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return false;

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsuarioJaPossuiJogoAsync(int idUsuario, int idJogo)
        {
            return await _context.JogosUsuarios
                .AnyAsync(j => j.IdUsuario == idUsuario && j.IdJogo == idJogo);
        }

        public async Task<bool> RegistrarCompraAsync(int idUsuario, int idJogo)
        {
            var compra = new JogosUsuarios
            {
                IdUsuario = idUsuario,
                IdJogo = idJogo,
                DataCompra = DateTime.UtcNow
            };

            _context.JogosUsuarios.Add(compra);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Jogo> AtualizaJogo(Jogo jogo)
        {
            _context.Update(jogo);
            await _context.SaveChangesAsync();
            return jogo;
        }
    }


}

