using CarCRM.Data;
using CarCRM.Extensions;
using CarCRM.Models;
using CarCRM.Models.Imagens;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarCRM.Controllers
{
    public class HomeController : Controller
    {
        private const int UsuarioIdFixo = 4;
        private readonly CarCRMImagemContext _contextImagem;
        private readonly CarCRMContext _context;
        public HomeController(CarCRMImagemContext contextImagem, CarCRMContext context)
        {
            _contextImagem = contextImagem;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditarPerfil()
        {
            var usuario = _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.Telefones)
                .FirstOrDefault(u => u.Id == UsuarioIdFixo);

            var fixo = usuario.Pessoa.Telefones.FirstOrDefault(t => t.TelefoneTipoId == 1);

            var celular = usuario.Pessoa.Telefones.FirstOrDefault(t => t.TelefoneTipoId == 2);

            if (usuario == null)
            {
                return NotFound();
            }

            var perfilViewModel = new PerfilViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Pessoa.Email,
                TelefoneDDD = fixo?.DDD,
                TelefoneNumero = fixo?.Numero,
                CelularDDD = celular?.DDD,
                CelularNumero = celular?.Numero,
            };

            UsuarioImagem imagemUsuario = _contextImagem.UsuarioImagem.Where(img => img.UsuarioId == 4).OrderBy(c => c.Id).LastOrDefault();
            if (imagemUsuario != null)
            {
                string imagemUsuarioBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(imagemUsuario.Imagem);
                ViewBag.ImagemUsuario = imagemUsuarioBase64;
            }
            return View(perfilViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarPerfil([FromForm] PerfilViewModel perfilViewModel)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Pessoa)
                    .ThenInclude(p => p.Telefones)
                .FirstOrDefault(u => u.Id == UsuarioIdFixo);

            if (usuario == null)
            {
                return NotFound();
            }

            var fixo = usuario.Pessoa.Telefones.FirstOrDefault(t => t.TelefoneTipoId == 1);

            var celular = usuario.Pessoa.Telefones.FirstOrDefault(t => t.TelefoneTipoId == 2);

            usuario.Pessoa.Nome = perfilViewModel.Nome;
            usuario.Pessoa.Email = perfilViewModel.Email;
            fixo?.DDD = perfilViewModel.TelefoneDDD;
            fixo?.Numero = perfilViewModel.TelefoneNumero;
            celular?.DDD = perfilViewModel.CelularDDD;
            celular?.Numero = perfilViewModel.CelularNumero;
            usuario.Senha = perfilViewModel.SenhaAtual;
            usuario.ConfirmaSenha = perfilViewModel.ConfirmarSenha;

            var imagemByte = await perfilViewModel.FotoPerfil.GetBytes();
            var usuarioImagem = new UsuarioImagem();
            usuarioImagem.UsuarioId = 4;
            usuarioImagem.Imagem = imagemByte;
            _contextImagem.UsuarioImagem.Add(usuarioImagem);
            _contextImagem.SaveChanges();
            TempData["MensagemSucesso"] = "Imagem atualizada com sucesso!";

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Veiculos");
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
