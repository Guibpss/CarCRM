using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

public class VeiculoVersoesController : Controller
 {
    private readonly CarCRMContext _context;

    public VeiculoVersoesController(CarCRMContext context)
    {
        _context = context;
    }

    //GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.VeiculoVersao.ToListAsync());
    }

    //GET/DETAILS
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();

        }

        var veiculoversao = await _context.VeiculoVersao.FirstOrDefaultAsync(m => m.Id == id);
        if (veiculoversao == null)
        {
            return NotFound();
        }

        return View(veiculoversao);
    }

    //GET/CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VeiculoVersaoViewModel veiculoversao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(veiculoversao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(veiculoversao);
    }

    //GET/EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculoversao = await _context.VeiculoVersao.FindAsync(id);
        if (veiculoversao == null)
        {
            return NotFound();
        }
        return View(veiculoversao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, VeiculoVersaoViewModel veiculoversao)
    {
        if (id != veiculoversao.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(veiculoversao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoVersaoExists(veiculoversao.Id))
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
        return View(veiculoversao);
    }

    //GET/DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculoversao = await _context.VeiculoVersao.FirstOrDefaultAsync(m => m.Id == id);
        if (veiculoversao == null)
        {
            return NotFound();
        }

        return View(veiculoversao);
    }

    //POST/DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var veiculoversao = await _context.VeiculoVersao.FindAsync(id);
        if (veiculoversao != null)
        {
            _context.VeiculoVersao.Remove(veiculoversao);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoVersaoExists(int? id)
    {
        return _context.VeiculoVersao.Any(e => e.Id == id);
    }

}
