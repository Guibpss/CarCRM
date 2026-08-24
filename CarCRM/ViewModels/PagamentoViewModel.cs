using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class PagamentoViewModel : ViewModelBase
    {
        public float Valor { get; set; }

        public int Parcelas { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }

        public int StatusPagamentoId { get; set; }

        public StatusPagamentoViewModel? StatusPagamento { get; set; }

        public int MetodoPagamentoId { get; set; }

        public MetodoPagamentoViewModel? MetodoPagamento { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }
    }
}
