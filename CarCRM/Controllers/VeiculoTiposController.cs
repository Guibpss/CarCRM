
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoTiposController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoTiposController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VEICULOTIPOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.VeiculoTipos.ToListAsync());
    }

    // GET: VEICULOTIPOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculotipo = await _context.VeiculoTipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculotipo == null)
        {
            return NotFound();
        }

        return View(veiculotipo);
    }

    // GET: VEICULOTIPOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VEICULOTIPOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] VeiculoTipo veiculotipo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(veiculotipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(veiculotipo);
    }

    // GET: VEICULOTIPOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculotipo = await _context.VeiculoTipos.FindAsync(id);
        if (veiculotipo == null)
        {
            return NotFound();
        }
        return View(veiculotipo);
    }

    // POST: VEICULOTIPOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] VeiculoTipo veiculotipo)
    {
        if (id != veiculotipo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(veiculotipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoTipoExists(veiculotipo.Id))
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
        return View(veiculotipo);
    }

    // GET: VEICULOTIPOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculotipo = await _context.VeiculoTipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculotipo == null)
        {
            return NotFound();
        }

        return View(veiculotipo);
    }

    // POST: VEICULOTIPOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var veiculotipo = await _context.VeiculoTipos.FindAsync(id);
        if (veiculotipo != null)
        {
            _context.VeiculoTipos.Remove(veiculotipo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoTipoExists(int? id)
    {
        return _context.VeiculoTipos.Any(e => e.Id == id);
    }
}
