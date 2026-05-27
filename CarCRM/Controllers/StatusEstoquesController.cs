
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class StatusEstoquesController : Controller
{
    private readonly CarCRMContext _context;

    public StatusEstoquesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: STATUSESTOQUES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.StatusEstoques.ToListAsync());
    }

    // GET: STATUSESTOQUES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusestoque = await _context.StatusEstoques
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusestoque == null)
        {
            return NotFound();
        }

        return View(statusestoque);
    }

    // GET: STATUSESTOQUES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STATUSESTOQUES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] StatusEstoque statusestoque)
    {
        if (ModelState.IsValid)
        {
            _context.Add(statusestoque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(statusestoque);
    }

    // GET: STATUSESTOQUES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusestoque = await _context.StatusEstoques.FindAsync(id);
        if (statusestoque == null)
        {
            return NotFound();
        }
        return View(statusestoque);
    }

    // POST: STATUSESTOQUES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Ativo")] StatusEstoque statusestoque)
    {
        if (id != statusestoque.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(statusestoque);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusEstoqueExists(statusestoque.Id))
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
        return View(statusestoque);
    }

    // GET: STATUSESTOQUES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusestoque = await _context.StatusEstoques
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusestoque == null)
        {
            return NotFound();
        }

        return View(statusestoque);
    }

    // POST: STATUSESTOQUES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statusestoque = await _context.StatusEstoques.FindAsync(id);
        if (statusestoque != null)
        {
            _context.StatusEstoques.Remove(statusestoque);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StatusEstoqueExists(int? id)
    {
        return _context.StatusEstoques.Any(e => e.Id == id);
    }
}
