using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaJuridicaViewModel : PessoaViewModel
    {
        [Required(ErrorMessage = "O campo Razão Social é obrigatório")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo Nome Fantasia é obrigatório")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo Nome Interno é obrigatório")]
        public string NomeInterno { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        public string CNPJ { get; set; }
    }
}
