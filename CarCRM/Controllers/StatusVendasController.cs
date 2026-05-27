
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class StatusVendasController : Controller
{
    private readonly CarCRMContext _context;

    public StatusVendasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: STATUSVENDAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.StatusVendas.ToListAsync());
    }

    // GET: STATUSVENDAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusvenda == null)
        {
            return NotFound();
        }

        return View(statusvenda);
    }

    // GET: STATUSVENDAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STATUSVENDAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] StatusVenda statusvenda)
    {
        if (ModelState.IsValid)
        {
            _context.Add(statusvenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(statusvenda);
    }

    // GET: STATUSVENDAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas.FindAsync(id);
        if (statusvenda == null)
        {
            return NotFound();
        }
        return View(statusvenda);
    }

    // POST: STATUSVENDAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Ativo")] StatusVenda statusvenda)
    {
        if (id != statusvenda.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(statusvenda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusVendaExists(statusvenda.Id))
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
        return View(statusvenda);
    }

    // GET: STATUSVENDAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statusvenda = await _context.StatusVendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statusvenda == null)
        {
            return NotFound();
        }

        return View(statusvenda);
    }

    // POST: STATUSVENDAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statusvenda = await _context.StatusVendas.FindAsync(id);
        if (statusvenda != null)
        {
            _context.StatusVendas.Remove(statusvenda);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StatusVendaExists(int? id)
    {
        return _context.StatusVendas.Any(e => e.Id == id);
    }
}
