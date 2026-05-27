namespace CarCRM.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
