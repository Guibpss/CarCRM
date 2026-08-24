using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class TelefoneViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo DDD é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O DDD deve ter {1} dígitos")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "DDD inválido (ex.: 11)")]
        public string DDD { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Número é obrigatório")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O Número deve ter entre {2} e {1} caracteres")]
        [RegularExpression(@"^\d{4,5}-?\d{4}$", ErrorMessage = "Número inválido (ex.: 999998888 ou 99999-8888)")]
        public string Numero { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Tipo de Telefone")]
        public int TelefoneTipoId { get; set; }

        public TelefoneTipoViewModel? TelefoneTipo { get; set; }

        public int PessoaId { get; set; }

        public PessoaViewModel? Pessoa { get; set; }
    }
}
