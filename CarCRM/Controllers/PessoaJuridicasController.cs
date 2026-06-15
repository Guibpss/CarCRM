
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class PessoaJuridicasController : Controller
{
    private readonly CarCRMContext _context;

    public PessoaJuridicasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PESSOAJURIDICAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.PessoasJuridica.ToListAsync());
    }

    // GET: PESSOAJURIDICAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoajuridica = await _context.PessoasJuridica
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoajuridica == null)
        {
            return NotFound();
        }

        return View(pessoajuridica);
    }

    // GET: PESSOAJURIDICAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PESSOAJURIDICAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RazaoSocial,NomeFantasia,NomeInterno,CNPJ,Nome,Email,Id,CriadoEm,Excluido")] PessoaJuridica pessoajuridica)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pessoajuridica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pessoajuridica);
    }

    // GET: PESSOAJURIDICAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoajuridica = await _context.PessoasJuridica.FindAsync(id);
        if (pessoajuridica == null)
        {
            return NotFound();
        }
        return View(pessoajuridica);
    }

    // POST: PESSOAJURIDICAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("RazaoSocial,NomeFantasia,NomeInterno,CNPJ,Nome,Email,Id,CriadoEm,Excluido")] PessoaJuridica pessoajuridica)
    {
        if (id != pessoajuridica.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pessoajuridica);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaJuridicaExists(pessoajuridica.Id))
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
        return View(pessoajuridica);
    }

    // GET: PESSOAJURIDICAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pessoajuridica = await _context.PessoasJuridica
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pessoajuridica == null)
        {
            return NotFound();
        }

        return View(pessoajuridica);
    }

    // POST: PESSOAJURIDICAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pessoajuridica = await _context.PessoasJuridica.FindAsync(id);
        if (pessoajuridica != null)
        {
            _context.PessoasJuridica.Remove(pessoajuridica);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PessoaJuridicaExists(int? id)
    {
        return _context.PessoasJuridica.Any(e => e.Id == id);
    }
}
