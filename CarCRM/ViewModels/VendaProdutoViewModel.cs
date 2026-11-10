using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class VendaProdutoViewModel : ViewModelBase
    {
        public DateTime DataVenda { get; set; }

        public float ValorVenda { get; set; }

        public float Desconto { get; set; }

        public int ClienteId { get; set; }

        public ClienteViewModel Cliente { get; set; }

        public int StatusVendaId { get; set; }

        public StatusVendaViewModel StatusVenda { get; set; }
        public int ProdutoId { get; set; }

        public float ValorFinal
        {
            get
            {
                return ValorVenda - Desconto;
            }
        }
    }
}
