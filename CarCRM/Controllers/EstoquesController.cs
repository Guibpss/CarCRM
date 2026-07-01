
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class EstoquesController : Controller
{
    private readonly CarCRMContext _context;

    public EstoquesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: ESTOQUES
    public async Task<IActionResult> Index()    
    {

        return View(await _context.Estoques.ToListAsync());
    }

    // GET: ESTOQUES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estoque = await _context.Estoques
            .FirstOrDefaultAsync(m => m.Id == id);
        if (estoque == null)
        {
            return NotFound();
        }

        return View(estoque);
    }

    // GET: ESTOQUES/Create
    public IActionResult Create()
    {
        var estoque = new Estoque();
        return View(estoque);
    }

    // POST: ESTOQUES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string nome, DateTime dataEntrada, bool excluido)
    {
        var estoque = new Estoque();
        estoque.Nome = nome;
        estoque.DataEntrada = dataEntrada;
        estoque.CriadoEm = DateTime.Now;
        estoque.Excluido = excluido;

            _context.Add(estoque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
      
        return View(estoque);
    }

    // GET: ESTOQUES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null)
        {
            return NotFound();
        }
        return View(estoque);
    }

    // POST: ESTOQUES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, string nome, DateTime dataEntrada, bool excluido)
    {
       
        if (id == null)
        {
            return NotFound();
        }

        var estoque = await _context.Estoques.FindAsync(id);

        if (estoque == null)
        {
            return NotFound();
        }

        estoque.Nome = nome;
        estoque.DataEntrada = dataEntrada;
        estoque.Excluido = excluido;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ESTOQUES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estoque = await _context.Estoques
            .FirstOrDefaultAsync(m => m.Id == id);
        if (estoque == null)
        {
            return NotFound();
        }

        return View(estoque);
    }

    // POST: ESTOQUES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque != null)
        {
            _context.Estoques.Remove(estoque);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EstoqueExists(int? id)
    {
        return _context.Estoques.Any(e => e.Id == id);
    }
}
