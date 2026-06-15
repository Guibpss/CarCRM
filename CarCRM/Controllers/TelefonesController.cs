
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class TelefonesController : Controller
{
    private readonly CarCRMContext _context;

    public TelefonesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: TELEFONES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Telefones.ToListAsync());
    }

    // GET: TELEFONES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefone = await _context.Telefones
            .FirstOrDefaultAsync(m => m.Id == id);
        if (telefone == null)
        {
            return NotFound();
        }

        return View(telefone);
    }

    // GET: TELEFONES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TELEFONES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,DDD,Numero,TelefoneTipoId,TelefoneTipo,PessoaId,Pessoa")] Telefone telefone)
    {
        if (ModelState.IsValid)
        {
            _context.Add(telefone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(telefone);
    }

    // GET: TELEFONES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefone = await _context.Telefones.FindAsync(id);
        if (telefone == null)
        {
            return NotFound();
        }
        return View(telefone);
    }

    // POST: TELEFONES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,DDD,Numero,TelefoneTipoId,TelefoneTipo,PessoaId,Pessoa")] Telefone telefone)
    {
        if (id != telefone.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(telefone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefoneExists(telefone.Id))
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
        return View(telefone);
    }

    // GET: TELEFONES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var telefone = await _context.Telefones
            .FirstOrDefaultAsync(m => m.Id == id);
        if (telefone == null)
        {
            return NotFound();
        }

        return View(telefone);
    }

    // POST: TELEFONES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var telefone = await _context.Telefones.FindAsync(id);
        if (telefone != null)
        {
            _context.Telefones.Remove(telefone);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TelefoneExists(int? id)
    {
        return _context.Telefones.Any(e => e.Id == id);
    }
}
