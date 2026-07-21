using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class TelefoneViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo DDD é obrigatório")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório")]
        public string Numero { get; set; }

        public int TelefoneTipoId { get; set; }
        public TelefoneTipoViewModel TelefoneTipo { get; set; }
        public int PessoaId { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}
