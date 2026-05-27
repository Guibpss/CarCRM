
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class PessoasController : Controller
{
    private readonly CarCRMContext _context;

    public PessoasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PESSOAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Pessoas.ToListAsync());
    }

    // GET: PESSOAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoa = await _context.Pessoas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoa == null)
        {
            return NotFound();
        }

        return View(pessoa);
    }

    // GET: PESSOAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PESSOAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Pessoa pessoa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pessoa);
    }

    // GET: PESSOAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }
        return View(pessoa);
    }

    // POST: PESSOAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Email")] Pessoa pessoa)
    {
        if (id != pessoa.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pessoa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(pessoa.Id))
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
        return View(pessoa);
    }

    // GET: PESSOAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoa = await _context.Pessoas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoa == null)
        {
            return NotFound();
        }

        return View(pessoa);
    }

    // POST: PESSOAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PessoaExists(int? id)
    {
        return _context.Pessoas.Any(e => e.Id == id);
    }
}
