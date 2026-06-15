
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoModelosController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoModelosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VEICULOMODELOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.VeiculoModelo.ToListAsync());
    }

    // GET: VEICULOMODELOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomodelo = await _context.VeiculoModelo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculomodelo == null)
        {
            return NotFound();
        }

        return View(veiculomodelo);
    }

    // GET: VEICULOMODELOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VEICULOMODELOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,VeiculoMarcaId,VeiculoMarca")] VeiculoModelo veiculomodelo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(veiculomodelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(veiculomodelo);
    }

    // GET: VEICULOMODELOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomodelo = await _context.VeiculoModelo.FindAsync(id);
        if (veiculomodelo == null)
        {
            return NotFound();
        }
        return View(veiculomodelo);
    }

    // POST: VEICULOMODELOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,VeiculoMarcaId,VeiculoMarca")] VeiculoModelo veiculomodelo)
    {
        if (id != veiculomodelo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(veiculomodelo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoModeloExists(veiculomodelo.Id))
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
        return View(veiculomodelo);
    }

    // GET: VEICULOMODELOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculomodelo = await _context.VeiculoModelo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculomodelo == null)
        {
            return NotFound();
        }

        return View(veiculomodelo);
    }

    // POST: VEICULOMODELOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var veiculomodelo = await _context.VeiculoModelo.FindAsync(id);
        if (veiculomodelo != null)
        {
            _context.VeiculoModelo.Remove(veiculomodelo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoModeloExists(int? id)
    {
        return _context.VeiculoModelo.Any(e => e.Id == id);
    }
}
