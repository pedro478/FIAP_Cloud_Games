using System.ComponentModel.DataAnnotations;

namespace FIAP_Cloud_Games.Domain.Entities
{
    public class Jogo
    {
        [Key]
        public int IdJogo { get; set; } 

        public string Nome { get; set; }  = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public DateTime DataLancamento { get; set; }

        public bool Disponivel { get; set; }
    }
}
