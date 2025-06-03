using FIAP_Cloud_Games.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FIAP_Cloud_Games.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public tipoAcesso tipoAcesso { get; set; } = tipoAcesso.Operador;

        public string SenhaHash { get; set; } = string.Empty;

        public virtual ICollection<Jogo> Jogos { get; set; }

    }
}
