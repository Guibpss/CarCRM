using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class EstoqueViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Data de Entrada é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }
    }
}
