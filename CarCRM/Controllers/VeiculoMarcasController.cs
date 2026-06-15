
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoMarcasController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoMarcasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VEICULOMARCAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.veiculoMarcas.ToListAsync());
    }

    // GET: VEICULOMARCAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomarca = await _context.veiculoMarcas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculomarca == null)
        {
            return NotFound();
        }

        return View(veiculomarca);
    }

    // GET: VEICULOMARCAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VEICULOMARCAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] VeiculoMarca veiculomarca)
    {
        if (ModelState.IsValid)
        {
            _context.Add(veiculomarca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(veiculomarca);
    }

    // GET: VEICULOMARCAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomarca = await _context.veiculoMarcas.FindAsync(id);
        if (veiculomarca == null)
        {
            return NotFound();
        }
        return View(veiculomarca);
    }

    // POST: VEICULOMARCAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] VeiculoMarca veiculomarca)
    {
        if (id != veiculomarca.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(veiculomarca);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoMarcaExists(veiculomarca.Id))
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
        return View(veiculomarca);
    }

    // GET: VEICULOMARCAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomarca = await _context.veiculoMarcas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculomarca == null)
        {
            return NotFound();
        }

        return View(veiculomarca);
    }

    // POST: VEICULOMARCAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var veiculomarca = await _context.veiculoMarcas.FindAsync(id);
        if (veiculomarca != null)
        {
            _context.veiculoMarcas.Remove(veiculomarca);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoMarcaExists(int? id)
    {
        return _context.veiculoMarcas.Any(e => e.Id == id);
    }
}
