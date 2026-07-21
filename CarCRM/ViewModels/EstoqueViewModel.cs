using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class EstoqueViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Data de Entrada é obrigatório")]
        public DateTime DataEntrada { get; set; }
    }
}
