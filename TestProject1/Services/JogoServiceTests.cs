using FIAP_Cloud_Games.Application.Services;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.DTOs;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Services
{
    public class JogoServiceTests
    {

        [Fact]
        public async Task ComprarJogo_DeveAdicionarJogoAoUsuario()
        {
            // Arrange
            var mockJogoRepo = new Mock<IJogoRepository>();
            var mockUsuarioRepo = new Mock<IUsuarioRepository>();

            var jogo = new Jogo { IdJogo = 1, Nome = "string", Disponivel = true };
            var usuario = new Usuario { IdUsuario = 1, Nome = "Admin", email = "admin@admin.com" };

            mockJogoRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(jogo);
            mockUsuarioRepo.Setup(x => x.GetByEmailAsync(usuario.email)).ReturnsAsync(usuario);

            var service = new JogoService(mockJogoRepo.Object, mockUsuarioRepo.Object);

            ComprarJogoDTO DTO = new ComprarJogoDTO { EmailUsuario = usuario.email, JogoID = jogo.IdJogo };

            // Act
            var resultado = await service.ComprarJogo(DTO);

            // Assert
            resultado.Should().NotBeNull();
            resultado.IdJogo.Should().Be(jogo.IdJogo);
            resultado.Nome.Should().Be(jogo.Nome);
        }

    }
}
