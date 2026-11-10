using CarCRM.Data;
using CarCRM.Extensions;
using CarCRM.Models;
using CarCRM.Models.Imagens;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarCRM.Controllers
{
    public class HomeController : Controller
    {
        //private const string UsuarioIdFixo = "4";
        private readonly CarCRMImagemContext _contextImagem;
        private readonly CarCRMContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public HomeController(CarCRMImagemContext contextImagem, CarCRMContext context, 
                              UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _contextImagem = contextImagem;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditarPerfil()
        {
            var usuarioId = _userManager.GetUserId(User);

            var usuario = _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.Telefones)
                .FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || usuario.Pessoa == null)
            {
                return NotFound();
            }

            var fixo = usuario.Pessoa.Telefones?.FirstOrDefault(t => t.TelefoneTipoId == 1);

            var celular = usuario.Pessoa.Telefones?.FirstOrDefault(t => t.TelefoneTipoId == 2);

            var perfilViewModel = new MeuPerfilViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Pessoa.Email,
                TelefoneDDD = fixo?.DDD,
                TelefoneNumero = fixo?.Numero,
                CelularDDD = celular?.DDD,
                CelularNumero = celular?.Numero,
            };

            var imagemUsuario = _contextImagem.UsuarioImagem
                .Where(img => img.UsuarioId == usuario.Id)
                .OrderBy(c => c.Id)
                .LastOrDefault();

            if (imagemUsuario != null)
            {
                string imagemUsuarioBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(imagemUsuario.Imagem);
                ViewBag.ImagemUsuario = imagemUsuarioBase64;
            }
            return View(perfilViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil([FromForm] MeuPerfilViewModel perfilViewModel)
        {
            var usuarioId = _userManager.GetUserId(User);

            var usuario = _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.Telefones)
                .FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || usuario.Pessoa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(perfilViewModel);
            }

            usuario.Nome = perfilViewModel.Nome.Trim();
            usuario.Pessoa.Nome = perfilViewModel.Nome.Trim();

            var novoEmail = perfilViewModel.Email.Trim();
            if (!string.Equals(usuario.Email, novoEmail, StringComparison.OrdinalIgnoreCase))
            {
                var emailResult = await _userManager.SetEmailAsync(usuario, novoEmail);
                var userNameResult = emailResult.Succeeded ? await _userManager.SetUserNameAsync(usuario, novoEmail) : emailResult;

                if (!userNameResult.Succeeded)
                {
                    foreach (var error in userNameResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View(perfilViewModel);
                }

                
            }

            usuario.Pessoa.Email = novoEmail;
            //Telefone - Fixo
            AtualizarTelefone(usuario.Pessoa, 1, perfilViewModel.TelefoneDDD, perfilViewModel.TelefoneNumero);

            //Telefone - Celular
            AtualizarTelefone(usuario.Pessoa, 2, perfilViewModel.CelularDDD, perfilViewModel.CelularNumero);

            await _context.SaveChangesAsync();
           
            if (perfilViewModel.FotoPerfil != null)
            {
                var imagemByte = await perfilViewModel.FotoPerfil.GetBytes();
                _contextImagem.UsuarioImagem.Add(new UsuarioImagem
                {
                    UsuarioId = usuario.Id,
                    Imagem = imagemByte
                });
                _contextImagem.SaveChangesAsync();
                    
            }

            if (!string.IsNullOrWhiteSpace(perfilViewModel.NovaSenha))
            {
                var senhaResult = await _userManager.ChangePasswordAsync(usuario, perfilViewModel.SenhaAtual, perfilViewModel.NovaSenha);

                if (!senhaResult.Succeeded)
                {
                    foreach (var error in senhaResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View(perfilViewModel);
                }
            }

            await _signInManager.RefreshSignInAsync(usuario);
            TempData["MensagemSucesso"] = "Perfil atualizado com sucesso!";

            _context.Usuarios.Update(usuario);
            return RedirectToAction("Index", "Veiculos");
        }

        public void AtualizarTelefone(PessoaFisica pessoa, int tipoId, string? ddd, string? numero)
        {
            pessoa.Telefones ??= new List<Telefone>();
            var telefone = pessoa.Telefones.FirstOrDefault(t => t.TelefoneTipoId == tipoId);

            if (string.IsNullOrWhiteSpace(numero))
            {
                return;
            }

            if (telefone == null)
            {
                pessoa.Telefones.Add(new Telefone
                {
                    TelefoneTipoId = tipoId,
                    DDD = ddd,
                    Numero = numero
                });
            }
            else 
            {
                telefone.DDD = ddd;
                telefone.Numero = numero;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

    }
}
