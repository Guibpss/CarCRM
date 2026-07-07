using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class CargoViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O limite do campo Nome é 100 caracteres")]
        [MinLength(5, ErrorMessage = "O limite mínimo do campo Nome é 5 caracteres")]
        public string Nome { get; set; }
    }
}