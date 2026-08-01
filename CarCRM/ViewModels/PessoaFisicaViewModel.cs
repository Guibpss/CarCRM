using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PessoaFisicaViewModel : PessoaViewModel
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo RG é obrigatório")]
        public string RG { get; set; }

        public DateTime DataNascimento { get; set; }

    }
}
