namespace CarCRM.Models
{
    public class Compra : EntidadeBase
    {
        public DateTime DataCompra { get; set; }

        public float ValorCompra { get; set; }

        public float Desconto { get; set; }

        public int VendedorId { get; set; }

        public Usuario Vendedor { get; set; }

        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }

        public int StatusCompraId { get; set; }

        public StatusCompra StatusCompra { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorCompra - Desconto;
            }
        }

        public virtual ICollection<Pagamento>? Pagamentos { get; set; }
    }
}
