
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VeiculoCombustiveisController : Controller
{
    private readonly CarCRMContext _context;

    public VeiculoCombustiveisController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VeiculoCombustivelS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.VeiculoCombustivel.ToListAsync());
    }

    // GET: VeiculoCombustivelS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoCombustivel = await _context.VeiculoCombustivel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (VeiculoCombustivel == null)
        {
            return NotFound();
        }

        return View(VeiculoCombustivel);
    }

    // GET: VeiculoCombustivelS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VeiculoCombustivelS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] VeiculoCombustivel VeiculoCombustivel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(VeiculoCombustivel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(VeiculoCombustivel);
    }

    // GET: VeiculoCombustivelS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoCombustivel = await _context.VeiculoCombustivel.FindAsync(id);
        if (VeiculoCombustivel == null)
        {
            return NotFound();
        }
        return View(VeiculoCombustivel);
    }

    // POST: VeiculoCombustivelS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] VeiculoCombustivel VeiculoCombustivel)
    {
        if (id != VeiculoCombustivel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(VeiculoCombustivel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoCombustivelExists(VeiculoCombustivel.Id))
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
        return View(VeiculoCombustivel);
    }

    // GET: VeiculoCombustivelS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var VeiculoCombustivel = await _context.VeiculoCombustivel
            .FirstOrDefaultAsync(m => m.Id == id);
        if (VeiculoCombustivel == null)
        {
            return NotFound();
        }

        return View(VeiculoCombustivel);
    }

    // POST: VeiculoCombustivelS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var VeiculoCombustivel = await _context.VeiculoCombustivel.FindAsync(id);
        if (VeiculoCombustivel != null)
        {
            _context.VeiculoCombustivel.Remove(VeiculoCombustivel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VeiculoCombustivelExists(int? id)
    {
        return _context.VeiculoCombustivel.Any(e => e.Id == id);
    }
}
