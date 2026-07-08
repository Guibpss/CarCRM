
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
        var vendas = _context.Vendas
            .Include(v => v.Cliente).ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda);
        return View(await vendas.ToListAsync());
    }

    // GET: VENDAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
            .Include(v => v.Cliente).ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        return View(venda);
    }

    // GET: VENDAS/Create
    public IActionResult Create()
    {
        CarregarDropdowns();
        return View();
    }

    // POST: VENDAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DateTime dataVenda, float valorVenda, float desconto, int clienteId, int vendedorId, int statusVendaId)
    {
        var venda = new Venda
        {
            DataVenda = dataVenda,
            ValorVenda = valorVenda,
            Desconto = desconto,
            ClienteId = clienteId,
            VendedorId = vendedorId,
            StatusVendaId = statusVendaId
        };

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

        var venda = await _context.Vendas
                .Include(v => v.Cliente).ThenInclude(c => c.Pessoa)
                .Include(v => v.Vendedor)
                .Include(v => v.StatusVenda)
                .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        CarregarDropdowns();
        return View(venda);
    }

    // POST: VENDAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, DateTime dataVenda, float valorVenda, float desconto, int clienteId, int vendedorId, int statusVendaId)
    {
        if (id != null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas.FindAsync(id);

        if (venda == null)
        {
            return NotFound();
        }

        venda.DataVenda = dataVenda;
        venda.ValorVenda = valorVenda;
        venda.Desconto = desconto;
        venda.ClienteId = clienteId;
        venda.VendedorId = vendedorId;
        venda.StatusVendaId = statusVendaId;

        await _context.SaveChangesAsync();
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
            .Include(v => v.Cliente).ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda)
            .FirstOrDefaultAsync(v => v.Id == id);

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

    private void CarregarDropdowns()
    {
        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).ToList();
        ViewBag.Vendedores = _context.Usuarios.ToList();
        ViewBag.StatusVendas = _context.StatusVendas.ToList();
    }
}
