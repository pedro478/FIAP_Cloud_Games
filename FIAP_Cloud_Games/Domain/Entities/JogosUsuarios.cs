using System.ComponentModel.DataAnnotations.Schema;

namespace FIAP_Cloud_Games.Domain.Entities
{
    public class JogosUsuarios
    {

        public int Id { get; set; }

        [ForeignKey("IdUsuario")]
        public int IdUsuario { get; set;}

        [ForeignKey("IdJogo")]
        public int IdJogo { get; set; }

        public DateTime DataCompra{ get; set; }
    }
}
