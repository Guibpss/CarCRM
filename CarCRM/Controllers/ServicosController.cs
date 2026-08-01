using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
public class ServicosController : Controller
{
    private readonly CarCRMContext _context;

    public ServicosController(CarCRMContext context)
    {  
        _context = context; 
    
    }

    public async Task<IActionResult> Index()
    {
        var servicos = await _context.Servicos
            .Include(s => s.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(s => s.ServicoTipo)
            .ToListAsync();

        var servicosViewModel = servicos.Select(s => new ServicoViewModel
        {
            Id = s.Id,
            Nome = s.Nome,
            Valor = s.Valor,
            ClienteId = s.ClienteId,
            Cliente = new ClienteViewModel
            {
                Id = s.Cliente.Id,
                PessoaId = s.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Id = s.Cliente.Pessoa.Id,
                    Nome = s.Cliente.Pessoa.Nome,
                    Email = s.Cliente.Pessoa.Email
                }
            },
            ServicoTipoId = s.ServicoTipoId,
            ServicoTipo = new ServicoTipoViewModel
            {
                Id = s.ServicoTipo.Id,
                Nome = s.ServicoTipo.Nome
            }
        }).ToList();

        return View(servicosViewModel);
    }
}

