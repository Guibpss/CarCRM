using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(3, MinimumLength = 2, ErrorMessage = "DDD inválido.")]
        [Display(Name = "DDD")]
        public string? TelefoneDDD { get; set; }

        [StringLength(10, ErrorMessage = "Número inválido.")]
        [Display(Name = "Telefone")]
        public string? TelefoneNumero { get; set; }

        [StringLength(3, MinimumLength = 2, ErrorMessage = "DDD inválido.")]
        [Display(Name = "DDD")]
        public string? CelularDDD { get; set; }

        [StringLength(10, ErrorMessage = "Número inválido.")]
        [Display(Name = "Celular")]
        public string? CelularNumero { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string? SenhaAtual { get; set; }

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [Display(Name = "Nova senha")]
        public string? NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não conferem.")]
        [Display(Name = "Confirmar senha")]
        public string? ConfirmarSenha { get; set; }
        public IFormFile FotoPerfil { get; set; }
    }
}
