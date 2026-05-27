
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class FornecedorTipoesController : Controller
{
    private readonly CarCRMContext _context;

    public FornecedorTipoesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: FORNECEDORTIPOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.FornecedorTipos.ToListAsync());
    }

    // GET: FORNECEDORTIPOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedortipo = await _context.FornecedorTipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fornecedortipo == null)
        {
            return NotFound();
        }

        return View(fornecedortipo);
    }

    // GET: FORNECEDORTIPOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FORNECEDORTIPOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] FornecedorTipo fornecedortipo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fornecedortipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fornecedortipo);
    }

    // GET: FORNECEDORTIPOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedortipo = await _context.FornecedorTipos.FindAsync(id);
        if (fornecedortipo == null)
        {
            return NotFound();
        }
        return View(fornecedortipo);
    }

    // POST: FORNECEDORTIPOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] FornecedorTipo fornecedortipo)
    {
        if (id != fornecedortipo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(fornecedortipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorTipoExists(fornecedortipo.Id))
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
        return View(fornecedortipo);
    }

    // GET: FORNECEDORTIPOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedortipo = await _context.FornecedorTipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fornecedortipo == null)
        {
            return NotFound();
        }

        return View(fornecedortipo);
    }

    // POST: FORNECEDORTIPOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var fornecedortipo = await _context.FornecedorTipos.FindAsync(id);
        if (fornecedortipo != null)
        {
            _context.FornecedorTipos.Remove(fornecedortipo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FornecedorTipoExists(int? id)
    {
        return _context.FornecedorTipos.Any(e => e.Id == id);
    }
}
