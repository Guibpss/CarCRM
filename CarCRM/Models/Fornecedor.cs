namespace CarCRM.Models
{
    public class Fornecedor : Pessoa
    {
        public string Documento { get; set; }
        public int FornecedorTipoId { get; set; }

        public FornecedorTipo FornecedorTipo { get; set; }
    }
}
