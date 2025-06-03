using FIAP_Cloud_Games.Application.Services;
using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Entities.Enums;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.DTOs;
using FluentAssertions;
using Moq;


namespace TestProject1.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _mockRepo = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_mockRepo.Object);
        }

        [Fact]
        public async Task TesteCriaUsuario()
        {
            var dto = new UsuarioCreateDTO
            {
                nome = "João",
                email = "joao@teste.com",
                senha = "Senha@123",
               tipoAcesso = 0
            };

            var usuarioEsperado = new Usuario
            {
                IdUsuario = 1,
                Nome = dto.nome,
                email = dto.email,
                tipoAcesso = tipoAcesso.Operador,
                SenhaHash = "hash"
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Usuario>()))
                     .ReturnsAsync(usuarioEsperado);

            var resultado = await _service.CriarUsuario(dto);

            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().Be(1);
            resultado.email.Should().Be(dto.email);
        }

        [Fact]
        public async Task TesteGetUsuarioByID()
        {
            var usuario = new Usuario { IdUsuario = 1, Nome = "Maria", email = "maria@teste.com" };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(usuario);

            var resultado = await _service.GetUsuarioById(1);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Maria");
        }

        [Fact]
        public async Task TesteGetUsuarioPorEmail()
        {
            var usuario = new Usuario { IdUsuario = 2, email = "teste@teste.com" };

            _mockRepo.Setup(r => r.GetByEmailAsync("teste@teste.com"))
                     .ReturnsAsync(usuario);

            var resultado = await _service.GetUsuarioByEmail("teste@teste.com");

            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().Be(2);
        }

        [Fact]
        public async Task TesteDeletaUsuario()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1))
                     .ReturnsAsync(true);

            var resultado = await _service.DeletaUsuario(1);

            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task TesteValidaUsuario_SenhaCorreta()
        {
            var senha = "Senha@123";
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

            var usuario = new Usuario
            {
                IdUsuario = 1,
                email = "user@teste.com",
                SenhaHash = senhaHash
            };

            _mockRepo.Setup(r => r.GetByEmailAsync("user@teste.com"))
                     .ReturnsAsync(usuario);

            var loginDTO = new LoginRequestDTO
            {
                Email = "user@teste.com",
                Senha = senha
            };

            var resultado = await _service.ValidaUsuario(loginDTO);

            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().Be(1);
        }

        [Fact]
        public async Task TesteValidaUsuario_SenhaIncorreta()
        {
            var usuario = new Usuario
            {
                email = "x@x.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("SenhaCerta")
            };

            _mockRepo.Setup(r => r.GetByEmailAsync("x@x.com"))
                     .ReturnsAsync(usuario);

            var loginDTO = new LoginRequestDTO
            {
                Email = "x@x.com",
                Senha = "SenhaErrada"
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.ValidaUsuario(loginDTO));

            ex.Message.Should().Be("E-mail ou senha inválidos.");

        }

        [Fact]
        public async Task TesteGetListaUsuarios()
        {
            var lista = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, Nome = "U1" },
                new Usuario { IdUsuario = 2, Nome = "U2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(lista);

            var resultado = await _service.GetTodosUsuarios();

            resultado.Should().HaveCount(2);
        }
    }
}
