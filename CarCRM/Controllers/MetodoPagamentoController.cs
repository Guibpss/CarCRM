using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Pkcs;

public class MetodoPagamentoController : Controller
{
    private readonly CarCRMContext _context;

    public MetodoPagamentoController(CarCRMContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var metodoPagamento = await _context.MetodosPagamento.ToListAsync();
        var metodoPagamentoViewModel = metodoPagamento.Select(mp => new  MetodoPagamentoViewModel
        {
            Id = mp.Id,
            Nome = mp.Nome,
            Ativo = mp.Ativo,
        }).ToList();

        return View(metodoPagamentoViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodoPagamento = await _context.MetodosPagamento.FirstOrDefaultAsync(mp => mp.Id == id);

        if (metodoPagamento == null)
        {
            return NotFound();
        }

        var metodoPagamentoViewModel = new MetodoPagamentoViewModel
        {
            Id = metodoPagamento.Id,
            Nome = metodoPagamento.Nome,
            Ativo = metodoPagamento.Ativo,
        };

        return View(metodoPagamentoViewModel);

    }

    public IActionResult Create ()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(MetodoPagamentoViewModel metodoPagamentoViewModel)
    {
        if (ModelState.IsValid)
        {
            var metodopagamento = new MetodoPagamento
            {
                Nome = metodoPagamentoViewModel.Nome,
                Ativo = metodoPagamentoViewModel.Ativo
            };

            _context.Add(metodopagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(metodoPagamentoViewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodopagamento = await _context.MetodosPagamento.FindAsync(id);
        if (metodopagamento == null)
        {
            return NotFound();
        }

        var metodoPagamentoViewModel = new MetodoPagamentoViewModel
        {
            Id = metodopagamento.Id,
            Nome = metodopagamento.Nome,
            Ativo = metodopagamento.Ativo
        };

        return View(metodoPagamentoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, MetodoPagamentoViewModel metodoPagamentoViewModel)
    {
        if (id != metodoPagamentoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var metodopagamento = await _context.MetodosPagamento
                    .FirstOrDefaultAsync(mp => mp.Id == id);

                if (metodopagamento == null)
                {
                    return NotFound();
                }

                metodopagamento.Nome = metodoPagamentoViewModel.Nome;
                metodopagamento.Ativo = metodoPagamentoViewModel.Ativo;

                _context.Update(metodopagamento);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagamentoExists(metodoPagamentoViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                
            }
            return RedirectToAction(nameof(Index));
        }
        return View(metodoPagamentoViewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodopagamento = await _context.MetodosPagamento.FirstOrDefaultAsync(mp => mp.Id == id);
        if (metodopagamento == null)
        {
            return NotFound();
        }

        var metodoPagamentoViewModel = new MetodoPagamentoViewModel
        {
            Id = metodopagamento.Id,
            Nome = metodopagamento.Nome,
            Ativo = metodopagamento.Ativo
        };
        return View(metodoPagamentoViewModel);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var metodopagamento = await _context.MetodosPagamento.FindAsync(id);
        if (metodopagamento != null)
        {
            _context.MetodosPagamento.Remove(metodopagamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MetodoPagamentoExists(int? id)
    {
        return _context.MetodosPagamento.Any(mp => mp.Id == id);
    }
}