
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class StatusComissaosController : Controller
{
    private readonly CarCRMContext _context;

    public StatusComissaosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: STATUSCOMISSAOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.StatusComissoes.ToListAsync());
    }

    // GET: STATUSCOMISSAOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuscomissao = await _context.StatusComissoes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statuscomissao == null)
        {
            return NotFound();
        }

        return View(statuscomissao);
    }

    // GET: STATUSCOMISSAOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STATUSCOMISSAOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] StatusComissao statuscomissao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(statuscomissao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(statuscomissao);
    }

    // GET: STATUSCOMISSAOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuscomissao = await _context.StatusComissoes.FindAsync(id);
        if (statuscomissao == null)
        {
            return NotFound();
        }
        return View(statuscomissao);
    }

    // POST: STATUSCOMISSAOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Ativo")] StatusComissao statuscomissao)
    {
        if (id != statuscomissao.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(statuscomissao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusComissaoExists(statuscomissao.Id))
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
        return View(statuscomissao);
    }

    // GET: STATUSCOMISSAOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuscomissao = await _context.StatusComissoes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statuscomissao == null)
        {
            return NotFound();
        }

        return View(statuscomissao);
    }

    // POST: STATUSCOMISSAOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statuscomissao = await _context.StatusComissoes.FindAsync(id);
        if (statuscomissao != null)
        {
            _context.StatusComissoes.Remove(statuscomissao);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StatusComissaoExists(int? id)
    {
        return _context.StatusComissoes.Any(e => e.Id == id);
    }
}
