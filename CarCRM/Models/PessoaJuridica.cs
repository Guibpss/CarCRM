namespace CarCRM.Models
{
    public class PessoaJuridica : Pessoa
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeInterno { get; set; }
        public string CNPJ { get; set; }
    }
}
