
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;

public class StatusVendasController : Controller
{
    private readonly CarCRMContext _context;

    public StatusVendasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: STATUSVENDAS
    public async Task<IActionResult> Index()
    {
        var statusVendas = await _context.StatusVendas.ToListAsync();

        var statusVendasViewModel = statusVendas.Select(s => new StatusVendaViewModel
        {
            Id = s.Id,
            Nome = s.Nome,
            Ativo = s.Ativo
        }).ToList();

        return View(statusVendasViewModel);
    }

    // GET: STATUSVENDAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusvenda == null)
        {
            return NotFound();
        }

        var statusVendaViewModel = new StatusVendaViewModel
        {
            Id = statusvenda.Id,
            Nome = statusvenda.Nome,
            Ativo = statusvenda.Ativo
        };

        return View(statusVendaViewModel);
    }

    // GET: STATUSVENDAS/Create
    public IActionResult Create()
    {
        return View(new StatusVendaViewModel());
    }

    // POST: STATUSVENDAS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StatusVendaViewModel statusVendaViewModel)
    {
        if (ModelState.IsValid)
        {
            var statusvenda = new StatusVenda
            {
                Nome = statusVendaViewModel.Nome,
                Ativo = statusVendaViewModel.Ativo
            };

            _context.Add(statusvenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(statusVendaViewModel);
    }

    // GET: STATUSVENDAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas.FindAsync(id);
        if (statusvenda == null)
        {
            return NotFound();
        }

        var statusVendaViewModel = new StatusVendaViewModel
        {
            Id = statusvenda.Id,
            Nome = statusvenda.Nome,
            Ativo = statusvenda.Ativo
        };

        return View(statusVendaViewModel);
    }

    // POST: STATUSVENDAS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, StatusVendaViewModel statusVendaViewModel)
    {
        if (id != statusVendaViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var statusvenda = await _context.StatusVendas.FindAsync(id);

                if (statusvenda == null)
                {
                    return NotFound();
                }

                statusvenda.Nome = statusVendaViewModel.Nome;
                statusvenda.Ativo = statusVendaViewModel.Ativo;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusVendaExists(statusVendaViewModel.Id))
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
        return View(statusVendaViewModel);
    }

    // GET: STATUSVENDAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusvenda == null)
        {
            return NotFound();
        }

        var statusVendaViewModel = new StatusVendaViewModel
        {
            Id = statusvenda.Id,
            Nome = statusvenda.Nome,
            Ativo = statusvenda.Ativo
        };

        return View(statusVendaViewModel);
    }

    // POST: STATUSVENDAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statusvenda = await _context.StatusVendas.FindAsync(id);
        if (statusvenda != null)
        {
            _context.StatusVendas.Remove(statusvenda);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StatusVendaExists(int? id)
    {
        return _context.StatusVendas.Any(e => e.Id == id);
    }
}
