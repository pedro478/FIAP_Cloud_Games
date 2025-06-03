using FIAP_Cloud_Games.Domain.Entities;
using FIAP_Cloud_Games.Domain.Entities.Enums;

namespace FIAP_Cloud_Games.DTOs
{
    public class UsuarioReadDTO
    {
        public string nome { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public tipoAcesso tipoAcesso { get; set; } = tipoAcesso.Operador;

        public ICollection<Jogo> jogosComprados { get; set; } = new List<Jogo>();

    }
}
