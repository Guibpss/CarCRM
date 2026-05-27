
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class StatusPagamentoesController : Controller
{
    private readonly CarCRMContext _context;

    public StatusPagamentoesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: STATUSPAGAMENTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.StatusPagamentos.ToListAsync());
    }

    // GET: STATUSPAGAMENTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuspagamento = await _context.StatusPagamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statuspagamento == null)
        {
            return NotFound();
        }

        return View(statuspagamento);
    }

    // GET: STATUSPAGAMENTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STATUSPAGAMENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] StatusPagamento statuspagamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(statuspagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(statuspagamento);
    }

    // GET: STATUSPAGAMENTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuspagamento = await _context.StatusPagamentos.FindAsync(id);
        if (statuspagamento == null)
        {
            return NotFound();
        }
        return View(statuspagamento);
    }

    // POST: STATUSPAGAMENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Ativo")] StatusPagamento statuspagamento)
    {
        if (id != statuspagamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(statuspagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusPagamentoExists(statuspagamento.Id))
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
        return View(statuspagamento);
    }

    // GET: STATUSPAGAMENTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var statuspagamento = await _context.StatusPagamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (statuspagamento == null)
        {
            return NotFound();
        }

        return View(statuspagamento);
    }

    // POST: STATUSPAGAMENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var statuspagamento = await _context.StatusPagamentos.FindAsync(id);
        if (statuspagamento != null)
        {
            _context.StatusPagamentos.Remove(statuspagamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StatusPagamentoExists(int? id)
    {
        return _context.StatusPagamentos.Any(e => e.Id == id);
    }
}
