
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoCoresController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoCoresController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VEICULOCORS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.VeiculoCor.ToListAsync());
    }

    // GET: VEICULOCORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculocor = await _context.VeiculoCor
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculocor == null)
        {
            return NotFound();
        }

        return View(veiculocor);
    }

    // GET: VEICULOCORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VEICULOCORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] VeiculoCor veiculocor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(veiculocor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(veiculocor);
    }

    // GET: VEICULOCORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculocor = await _context.VeiculoCor.FindAsync(id);
        if (veiculocor == null)
        {
            return NotFound();
        }
        return View(veiculocor);
    }

    // POST: VEICULOCORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] VeiculoCor veiculocor)
    {
        if (id != veiculocor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(veiculocor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoCorExists(veiculocor.Id))
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
        return View(veiculocor);
    }

    // GET: VEICULOCORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var veiculocor = await _context.VeiculoCor
            .FirstOrDefaultAsync(m => m.Id == id);
        if (veiculocor == null)
        {
            return NotFound();
        }

        return View(veiculocor);
    }

    // POST: VEICULOCORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var veiculocor = await _context.VeiculoCor.FindAsync(id);
        if (veiculocor != null)
        {
            _context.VeiculoCor.Remove(veiculocor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoCorExists(int? id)
    {
        return _context.VeiculoCor.Any(e => e.Id == id);
    }
}
