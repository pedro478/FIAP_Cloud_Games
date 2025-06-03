namespace FIAP_Cloud_Games.DTOs
{
    public class editarJogoDTO
    {

        public int id {  get; set; }    
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public bool Disponivel { get; set; }

    }
}
