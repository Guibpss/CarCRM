
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class VendasController : Controller
{
    private readonly CarCRMContext _context;

    public VendasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VENDAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Vendas.ToListAsync());
    }

    // GET: VENDAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (venda == null)
        {
            return NotFound();
        }

        return View(venda);
    }

    // GET: VENDAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VENDAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,DataVenda,ValorVenda,Desconto,ValorFinal,CriadoEm,ClienteId,EstoqueId,FuncionarioId,StatusVendaId")] Venda venda)
    {
        if (ModelState.IsValid)
        {
            _context.Add(venda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(venda);
    }

    // GET: VENDAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null)
        {
            return NotFound();
        }
        return View(venda);
    }

    // POST: VENDAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,DataVenda,ValorVenda,Desconto,ValorFinal,CriadoEm,ClienteId,EstoqueId,FuncionarioId,StatusVendaId")] Venda venda)
    {
        if (id != venda.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(venda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(venda.Id))
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
        return View(venda);
    }

    // GET: VENDAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (venda == null)
        {
            return NotFound();
        }

        return View(venda);
    }

    // POST: VENDAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda != null)
        {
            _context.Vendas.Remove(venda);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VendaExists(int? id)
    {
        return _context.Vendas.Any(e => e.Id == id);
    }
}
