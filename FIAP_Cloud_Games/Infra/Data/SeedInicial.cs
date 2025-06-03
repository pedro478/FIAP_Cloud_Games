using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Entities.Enums;

namespace FIAP_Cloud_Games.Infra.Data
{
    public class SeedInicial
    {
        public static async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();

            if (!context.Usuarios.Any())
            {
                var usuario = new DTOs.UsuarioCreateDTO
                {
                    nome = "Admin",
                    email = "admin@admin.com",
                    senha = "Senha@2025", 
                    tipoAcesso = tipoAcesso.Administrador
                };

                await usuarioService.CriarUsuario(usuario); // Ou o que for equivalente no seu service
            }
        }


    }
}
