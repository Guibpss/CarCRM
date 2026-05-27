namespace CarCRM.Models
{
    public class Fornecedor : Pessoa
    {
        public int FornecedorTipoId { get; set; }

        public FornecedorTipo FornecedorTipo { get; set; }
    }
}
