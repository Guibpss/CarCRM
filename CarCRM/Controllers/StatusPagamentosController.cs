using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;

public class StatusPagamentosController : Controller
{
    private readonly CarCRMContext _context;

    public StatusPagamentosController(CarCRMContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var statusPagamentos = await _context.StatusPagamentos.ToListAsync();
        var statusPagamentosViewModel = statusPagamentos.Select(s => new StatusPagamentoViewModel
        {
            Id = s.Id,
            Nome = s.Nome,
            Ativo = s.Ativo
        }).ToList();
        return View(statusPagamentosViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusPagamento = await _context.StatusPagamentos.FirstOrDefaultAsync(m => m.Id == id);

        if (statusPagamento == null)
        {

            return NotFound();
        }

        var statusPagamentoViewModel = new StatusPagamentoViewModel
        {
            Id = statusPagamento.Id,
            Nome = statusPagamento.Nome,
            Ativo = statusPagamento.Ativo
        };

        return View(statusPagamentoViewModel);

    }

    public IActionResult Create ()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StatusPagamentoViewModel statusPagamentoViewModel)
    {
        if (ModelState.IsValid)
        {
            var statuspagamento = new StatusPagamento
            {
                Nome = statusPagamentoViewModel.Nome,
                Ativo = statusPagamentoViewModel.Ativo
            };

            _context.Add(statuspagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        return View(statusPagamentoViewModel);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuspagamento = await _context.StatusPagamentos.FindAsync(id);
        if (statuspagamento == null)
        {
            return NotFound();
        }

        var statusPagamentoViewModel = new StatusPagamentoViewModel
        {
            Id = statuspagamento.Id,
            Nome = statuspagamento.Nome,
            Ativo = statuspagamento.Ativo
        };

        return View(statusPagamentoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, StatusPagamentoViewModel statusPagamentoViewModel)
    {
        if (id != statusPagamentoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var statuspagamento = await _context.StatusPagamentos
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (statuspagamento == null)
                {
                    return NotFound();
                }

                statuspagamento.Nome = statusPagamentoViewModel.Nome;
                statuspagamento.Ativo = statusPagamentoViewModel.Ativo;

                _context.Update(statuspagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusPagamentoExists(statusPagamentoViewModel.Id))
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
        return View(statusPagamentoViewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var statuspagamento = await _context.StatusPagamentos.FirstOrDefaultAsync(m => m.Id == id);
        if (statuspagamento == null)
        {
            return NotFound();

        }

        var statusPagamentoViewModel = new StatusPagamentoViewModel
        {
            Id = statuspagamento.Id,
            Nome = statuspagamento.Nome,
            Ativo = statuspagamento.Ativo
        };
        return View(statusPagamentoViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statuspagamento = await _context.StatusPagamentos.FindAsync(id);
        if (statuspagamento != null)
        {
            _context.StatusPagamentos.Remove(statuspagamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool StatusPagamentoExists(int? id)
    {
        return _context.StatusPagamentos.Any(e => e.Id == id);
    }
}