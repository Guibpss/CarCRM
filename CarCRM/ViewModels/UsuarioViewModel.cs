using CarCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCRM.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Confirmação de Senha é obrigatório")]
        public string ConfirmaSenha { get; set; }

        public DateTime CriadoEm { get; set; }
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }
        public PerfilViewModel? Perfil { get; set; }
        public int PessoaId { get; set; }
        public PessoaFisicaViewModel Pessoa { get; set; } = new PessoaFisicaViewModel();
    }
}
