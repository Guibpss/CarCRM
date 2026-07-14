using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Excluido { get; set; }

        public int PessoaId { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}
