
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class ComissaosController : Controller
{
    private readonly CarCRMContext _context;

    public ComissaosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: COMISSAOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Comissoes.ToListAsync());
    }

    // GET: COMISSAOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comissao = await _context.Comissoes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comissao == null)
        {
            return NotFound();
        }

        return View(comissao);
    }

    // GET: COMISSAOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: COMISSAOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Percentual,ValorTotal,DataPagamento,CriadoEm,FuncionarioId,VendaId,StatusComissaoId")] Comissao comissao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comissao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comissao);
    }

    // GET: COMISSAOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comissao = await _context.Comissoes.FindAsync(id);
        if (comissao == null)
        {
            return NotFound();
        }
        return View(comissao);
    }

    // POST: COMISSAOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Percentual,ValorTotal,DataPagamento,CriadoEm,FuncionarioId,VendaId,StatusComissaoId")] Comissao comissao)
    {
        if (id != comissao.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(comissao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComissaoExists(comissao.Id))
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
        return View(comissao);
    }

    // GET: COMISSAOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comissao = await _context.Comissoes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comissao == null)
        {
            return NotFound();
        }

        return View(comissao);
    }

    // POST: COMISSAOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var comissao = await _context.Comissoes.FindAsync(id);
        if (comissao != null)
        {
            _context.Comissoes.Remove(comissao);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ComissaoExists(int? id)
    {
        return _context.Comissoes.Any(e => e.Id == id);
    }
}
