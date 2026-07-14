using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class TelefoneViewModel
    {
        public int Id { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public int TelefoneTipoId { get; set; }
        public TelefoneTipoViewModel TelefoneTipo { get; set; }
        public int PessoaId { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}
