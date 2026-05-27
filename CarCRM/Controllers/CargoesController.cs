
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class CargoesController : Controller
{
    private readonly CarCRMContext _context;

    public CargoesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: CARGOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cargos.ToListAsync());
    }

    // GET: CARGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cargo == null)
        {
            return NotFound();
        }

        return View(cargo);
    }

    // GET: CARGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CARGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] Cargo cargo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cargo);
    }

    // GET: CARGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
        {
            return NotFound();
        }
        return View(cargo);
    }

    // POST: CARGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] Cargo cargo)
    {
        if (id != cargo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cargo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(cargo.Id))
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
        return View(cargo);
    }

    // GET: CARGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cargo == null)
        {
            return NotFound();
        }

        return View(cargo);
    }

    // POST: CARGOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo != null)
        {
            _context.Cargos.Remove(cargo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CargoExists(int? id)
    {
        return _context.Cargos.Any(e => e.Id == id);
    }
}
