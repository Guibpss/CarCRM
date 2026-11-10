using System.ComponentModel.DataAnnotations;
namespace CarCRM.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Email")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        public bool LembrarMe { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
