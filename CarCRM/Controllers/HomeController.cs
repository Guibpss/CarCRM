using CarCRM.Data;
using CarCRM.Extensions;
using CarCRM.Models;
using CarCRM.Models.Imagens;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarCRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarCRMImagemContext _contextImagem;
        public HomeController(CarCRMImagemContext contextImagem)
        {
            _contextImagem = contextImagem;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditarPerfil()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditarPerfil([FromForm] PerfilViewModel perfilViewModel)
        {
            var imagemByte = await perfilViewModel.FotoPerfil.GetBytes();
            var usuarioImagem = new UsuarioImagem();
            usuarioImagem.UsuarioId = 4;
            usuarioImagem.Imagem = imagemByte;
            _contextImagem.UsuarioImagem.Add(usuarioImagem);
            _contextImagem.SaveChanges();
            return View();
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
