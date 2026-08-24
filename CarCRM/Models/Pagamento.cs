namespace CarCRM.Models
{
    public class Pagamento : EntidadeBase
    {
        public float Valor { get; set; }

        public int Parcelas { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }

        public int StatusPagamentoId { get; set; }

        public StatusPagamento? StatusPagamento { get; set; }

        public int MetodoPagamentoId { get; set; }

        public MetodoPagamento? MetodoPagamento { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }
    }
}