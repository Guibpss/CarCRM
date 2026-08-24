using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class ServicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [Range(0.01, 99_999_999, ErrorMessage = "O Valor deve estar entre {1} e {2}")]
        public float Valor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Tipo de Serviço")]
        public int ServicoTipoId { get; set; }

        public ServicoTipoViewModel? ServicoTipo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Cliente")]
        public int ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }
    }
}
