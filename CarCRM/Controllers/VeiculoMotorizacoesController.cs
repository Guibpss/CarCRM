
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoMotorizacoesController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoMotorizacoesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VeiculoMotorizacaoS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.VeiculoMotorizacao.ToListAsync());
    }

    // GET: VeiculoMotorizacaoS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoMotorizacao = await _context.VeiculoMotorizacao
            .FirstOrDefaultAsync(m => m.Id == id);
        if (VeiculoMotorizacao == null)
        {
            return NotFound();
        }

        return View(VeiculoMotorizacao);
    }

    // GET: VeiculoMotorizacaoS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VeiculoMotorizacaoS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] VeiculoMotorizacao VeiculoMotorizacao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(VeiculoMotorizacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(VeiculoMotorizacao);
    }

    // GET: VeiculoMotorizacaoS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoMotorizacao = await _context.VeiculoMotorizacao.FindAsync(id);
        if (VeiculoMotorizacao == null)
        {
            return NotFound();
        }
        return View(VeiculoMotorizacao);
    }

    // POST: VeiculoMotorizacaoS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] VeiculoMotorizacao VeiculoMotorizacao)
    {
        if (id != VeiculoMotorizacao.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(VeiculoMotorizacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoMotorizacaoExists(VeiculoMotorizacao.Id))
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
        return View(VeiculoMotorizacao);
    }

    // GET: VeiculoMotorizacaoS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoMotorizacao = await _context.VeiculoMotorizacao
            .FirstOrDefaultAsync(m => m.Id == id);
        if (VeiculoMotorizacao == null)
        {
            return NotFound();
        }

        return View(VeiculoMotorizacao);
    }

    // POST: VeiculoMotorizacaoS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var VeiculoMotorizacao = await _context.VeiculoMotorizacao.FindAsync(id);
        if (VeiculoMotorizacao != null)
        {
            _context.VeiculoMotorizacao.Remove(VeiculoMotorizacao);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoMotorizacaoExists(int? id)
    {
        return _context.VeiculoMotorizacao.Any(e => e.Id == id);
    }
}
