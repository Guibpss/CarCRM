
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class PerfilsController : Controller
{
    private readonly CarCRMContext _context;

    public PerfilsController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PERFILS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Perfis.ToListAsync());
    }

    // GET: PERFILS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    // GET: PERFILS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PERFILS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] Perfil perfil)
    {
        if (ModelState.IsValid)
        {
            _context.Add(perfil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(perfil);
    }

    // GET: PERFILS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil == null)
        {
            return NotFound();
        }
        return View(perfil);
    }

    // POST: PERFILS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] Perfil perfil)
    {
        if (id != perfil.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(perfil);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(perfil.Id))
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
        return View(perfil);
    }

    // GET: PERFILS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    // POST: PERFILS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil != null)
        {
            _context.Perfis.Remove(perfil);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PerfilExists(int? id)
    {
        return _context.Perfis.Any(e => e.Id == id);
    }
}
