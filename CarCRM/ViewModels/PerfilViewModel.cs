using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
    }
}
