
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class FornecedoresController : Controller
{
    private readonly CarCRMContext _context;

    public FornecedoresController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: FORNECEDORS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Fornecedores.ToListAsync());
    }

    // GET: FORNECEDORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fornecedor == null)
        {
            return NotFound();
        }

        return View(fornecedor);
    }

    // GET: FORNECEDORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FORNECEDORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FornecedorTipoId,FornecedorTipo,Nome,Email,Id,CriadoEm,Excluido")] Fornecedor fornecedor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fornecedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fornecedor);
    }

    // GET: FORNECEDORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor == null)
        {
            return NotFound();
        }
        return View(fornecedor);
    }

    // POST: FORNECEDORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("FornecedorTipoId,FornecedorTipo,Nome,Email,Id,CriadoEm,Excluido")] Fornecedor fornecedor)
    {
        if (id != fornecedor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(fornecedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(fornecedor.Id))
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
        return View(fornecedor);
    }

    // GET: FORNECEDORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fornecedor == null)
        {
            return NotFound();
        }

        return View(fornecedor);
    }

    // POST: FORNECEDORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FornecedorExists(int? id)
    {
        return _context.Fornecedores.Any(e => e.Id == id);
    }
}
