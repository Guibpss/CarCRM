namespace CarCRM.Models
{
    public class EntidadeBase
    {
        public EntidadeBase()
        {
            CriadoEm = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CriadoEm { get; set; }

        public bool Excluido { get; set; }
    }
}
