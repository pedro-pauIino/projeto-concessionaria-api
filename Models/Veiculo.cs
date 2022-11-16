namespace ProjetoEscola_API.Models
{
    public class Veiculo
    {
        public int id { get; set; }
        public int chassi { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public int ano { get; set; }
        public string? cor { get; set; }
        public int preco { get; set; }
    }
}