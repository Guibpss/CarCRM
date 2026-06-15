
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class PagamentosController : Controller
{
    private readonly CarCRMContext _context;

    public PagamentosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PAGAMENTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Pagamentos.ToListAsync());
    }

    // GET: PAGAMENTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pagamento == null)
        {
            return NotFound();
        }

        return View(pagamento);
    }

    // GET: PAGAMENTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PAGAMENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Valor,Parcelas,DataVencimento,DataPagamento,StatusPagamentoId,StatusPagamento,MetodoPagamentoId,MetodoPagamento,Id,CriadoEm,Excluido")] Pagamento pagamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pagamento);
    }

    // GET: PAGAMENTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos.FindAsync(id);
        if (pagamento == null)
        {
            return NotFound();
        }
        return View(pagamento);
    }

    // POST: PAGAMENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Valor,Parcelas,DataVencimento,DataPagamento,StatusPagamentoId,StatusPagamento,MetodoPagamentoId,MetodoPagamento,Id,CriadoEm,Excluido")] Pagamento pagamento)
    {
        if (id != pagamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(pagamento.Id))
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
        return View(pagamento);
    }

    // GET: PAGAMENTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pagamento == null)
        {
            return NotFound();
        }

        return View(pagamento);
    }

    // POST: PAGAMENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pagamento = await _context.Pagamentos.FindAsync(id);
        if (pagamento != null)
        {
            _context.Pagamentos.Remove(pagamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PagamentoExists(int? id)
    {
        return _context.Pagamentos.Any(e => e.Id == id);
    }
}
