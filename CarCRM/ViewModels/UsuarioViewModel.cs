using CarCRM.Models;

namespace CarCRM.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }
        public PerfilViewModel? Perfil { get; set; }
        public int PessoaId { get; set; }
        public PessoaFisicaViewModel Pessoa { get; set; } = new PessoaFisicaViewModel();
    }
}
