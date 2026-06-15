
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class MetodoPagamentosController : Controller
{
    private readonly CarCRMContext _context;

    public MetodoPagamentosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: METODOPAGAMENTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.MetodosPagamento.ToListAsync());
    }

    // GET: METODOPAGAMENTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodopagamento = await _context.MetodosPagamento
            .FirstOrDefaultAsync(m => m.Id == id);
        if (metodopagamento == null)
        {
            return NotFound();
        }

        return View(metodopagamento);
    }

    // GET: METODOPAGAMENTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: METODOPAGAMENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] MetodoPagamento metodopagamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(metodopagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(metodopagamento);
    }

    // GET: METODOPAGAMENTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodopagamento = await _context.MetodosPagamento.FindAsync(id);
        if (metodopagamento == null)
        {
            return NotFound();
        }
        return View(metodopagamento);
    }

    // POST: METODOPAGAMENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Ativo")] MetodoPagamento metodopagamento)
    {
        if (id != metodopagamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(metodopagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagamentoExists(metodopagamento.Id))
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
        return View(metodopagamento);
    }

    // GET: METODOPAGAMENTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var metodopagamento = await _context.MetodosPagamento
            .FirstOrDefaultAsync(m => m.Id == id);
        if (metodopagamento == null)
        {
            return NotFound();
        }

        return View(metodopagamento);
    }

    // POST: METODOPAGAMENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var metodopagamento = await _context.MetodosPagamento.FindAsync(id);
        if (metodopagamento != null)
        {
            _context.MetodosPagamento.Remove(metodopagamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MetodoPagamentoExists(int? id)
    {
        return _context.MetodosPagamento.Any(e => e.Id == id);
    }
}
