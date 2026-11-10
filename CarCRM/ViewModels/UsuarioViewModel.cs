using CarCRM.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class UsuarioViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um perfil")]
        [Display(Name = "Perfil")]
        public string? PerfilId { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "A Senha deve ter entre {2} e {1} caracteres")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "A Confirmação de Senha deve ser igual à Senha")]
        public string? ConfirmaSenha { get; set; }

        public DateTime CriadoEm { get; set; }

        public bool Ativo { get; set; }

        public int PessoaId { get; set; }

        public PessoaFisicaViewModel Pessoa { get; set; } = new PessoaFisicaViewModel();
    }
}
