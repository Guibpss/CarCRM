namespace CarCRM.Models
{
    public class Comissao : EntidadeBase
    {
        public float Percentual { get; set; }
        
        public int FuncionarioId { get; set; }
        
        public Funcionario Funcionario { get; set; }
        
        public int VendaId { get; set; }
        
        public Venda Venda { get; set; }

        public int StatusComissaoId { get; set; }
        
        public StatusComissao StatusComissao { get; set; }

        public int PagamentoId { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}