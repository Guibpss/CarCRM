
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class PessoaFisicasController : Controller
{
    private readonly CarCRMContext _context;

    public PessoaFisicasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PESSOAFISICAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.PessoasFisica.ToListAsync());
    }

    // GET: PESSOAFISICAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoafisica = await _context.PessoasFisica
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoafisica == null)
        {
            return NotFound();
        }

        return View(pessoafisica);
    }

    // GET: PESSOAFISICAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PESSOAFISICAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CPF,RG,DataNascimento,Nome,Email,Id,CriadoEm,Excluido")] PessoaFisica pessoafisica)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pessoafisica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pessoafisica);
    }

    // GET: PESSOAFISICAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoafisica = await _context.PessoasFisica.FindAsync(id);
        if (pessoafisica == null)
        {
            return NotFound();
        }
        return View(pessoafisica);
    }

    // POST: PESSOAFISICAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("CPF,RG,DataNascimento,Nome,Email,Id,CriadoEm,Excluido")] PessoaFisica pessoafisica)
    {
        if (id != pessoafisica.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pessoafisica);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaFisicaExists(pessoafisica.Id))
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
        return View(pessoafisica);
    }

    // GET: PESSOAFISICAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoafisica = await _context.PessoasFisica
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoafisica == null)
        {
            return NotFound();
        }

        return View(pessoafisica);
    }

    // POST: PESSOAFISICAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pessoafisica = await _context.PessoasFisica.FindAsync(id);
        if (pessoafisica != null)
        {
            _context.PessoasFisica.Remove(pessoafisica);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PessoaFisicaExists(int? id)
    {
        return _context.PessoasFisica.Any(e => e.Id == id);
    }
}
