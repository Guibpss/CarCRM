namespace CarCRM.Models
{
    public class Cliente 
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Excluido { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }


    }
}
