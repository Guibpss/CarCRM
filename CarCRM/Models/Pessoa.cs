namespace CarCRM.Models
{
    public class Pessoa : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}
