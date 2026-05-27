
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class TelefoneTipoesController : Controller
{
    private readonly CarCRMContext _context;

    public TelefoneTipoesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: TELEFONETIPOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.TelefonesTipo.ToListAsync());
    }

    // GET: TELEFONETIPOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefonetipo = await _context.TelefonesTipo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (telefonetipo == null)
        {
            return NotFound();
        }

        return View(telefonetipo);
    }

    // GET: TELEFONETIPOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TELEFONETIPOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] TelefoneTipo telefonetipo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(telefonetipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(telefonetipo);
    }

    // GET: TELEFONETIPOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefonetipo = await _context.TelefonesTipo.FindAsync(id);
        if (telefonetipo == null)
        {
            return NotFound();
        }
        return View(telefonetipo);
    }

    // POST: TELEFONETIPOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] TelefoneTipo telefonetipo)
    {
        if (id != telefonetipo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(telefonetipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefoneTipoExists(telefonetipo.Id))
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
        return View(telefonetipo);
    }

    // GET: TELEFONETIPOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefonetipo = await _context.TelefonesTipo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (telefonetipo == null)
        {
            return NotFound();
        }

        return View(telefonetipo);
    }

    // POST: TELEFONETIPOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var telefonetipo = await _context.TelefonesTipo.FindAsync(id);
        if (telefonetipo != null)
        {
            _context.TelefonesTipo.Remove(telefonetipo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TelefoneTipoExists(int? id)
    {
        return _context.TelefonesTipo.Any(e => e.Id == id);
    }
}
