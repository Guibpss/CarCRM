namespace CarCRM.Models
{
    public class Telefone
    {
        public int Id { get; set; } 
        public string DDD { get; set; }
        public string Numero { get; set; }
        public int TelefoneTipoId { get; set; }
        public TelefoneTipo TelefoneTipo { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
