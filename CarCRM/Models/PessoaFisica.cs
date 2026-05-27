using Newtonsoft.Json;

namespace CarCRM.Models
{
    public class PessoaFisica : Pessoa
    {
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
