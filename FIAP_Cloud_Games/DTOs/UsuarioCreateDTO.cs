using FIAP_Cloud_Games.Domain.Entities.Enums;

namespace FIAP_Cloud_Games.DTOs
{
    public class UsuarioCreateDTO
    {
        public string nome { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string senha {  get; set; } = string.Empty;
        public tipoAcesso tipoAcesso { get; set; }

    }
}
