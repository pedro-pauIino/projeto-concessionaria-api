namespace ProjetoEscola_API.Models
{
    public class Concessionaria
    {
        public int id { get; set; }
        public int codLoja { get; set; }
        public string? nomeLoja { get; set; }
        public int cep { get; set; }
        public string? endereco { get; set; }
        public string? estado { get; set; }
    }
}