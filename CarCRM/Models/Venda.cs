namespace CarCRM.Models
{
    public class Venda : EntidadeBase
    {
        public DateTime DataVenda {  get; set; }
        
        public float ValorVenda { get; set; }
        
        public float Desconto { get; set; }
        
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }

        public int StatusVendaId { get; set; }
        
        public StatusVenda StatusVenda { get; set; }

        public float ValorFinal 
        {
            get
            {
                return ValorVenda - Desconto;
            } 
        }

        public virtual ICollection<Pagamento>? Pagamentos { get; set; }
    }
}
