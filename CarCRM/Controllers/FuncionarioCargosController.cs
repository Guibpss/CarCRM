
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class FuncionarioCargosController : Controller
{
    private readonly CarCRMContext _context;

    public FuncionarioCargosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: FUNCIONARIOCARGOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.FuncionarioCargo.ToListAsync());
    }

    // GET: FUNCIONARIOCARGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionariocargo = await _context.FuncionarioCargo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (funcionariocargo == null)
        {
            return NotFound();
        }

        return View(funcionariocargo);
    }

    // GET: FUNCIONARIOCARGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FUNCIONARIOCARGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FuncionarioId,Funcionario,CargoId,Cargo,Vigente,DataInicio,DataIFim,Id,CriadoEm,Excluido")] FuncionarioCargo funcionariocargo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(funcionariocargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(funcionariocargo);
    }

    // GET: FUNCIONARIOCARGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionariocargo = await _context.FuncionarioCargo.FindAsync(id);
        if (funcionariocargo == null)
        {
            return NotFound();
        }
        return View(funcionariocargo);
    }

    // POST: FUNCIONARIOCARGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("FuncionarioId,Funcionario,CargoId,Cargo,Vigente,DataInicio,DataIFim,Id,CriadoEm,Excluido")] FuncionarioCargo funcionariocargo)
    {
        if (id != funcionariocargo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(funcionariocargo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioCargoExists(funcionariocargo.Id))
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
        return View(funcionariocargo);
    }

    // GET: FUNCIONARIOCARGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionariocargo = await _context.FuncionarioCargo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (funcionariocargo == null)
        {
            return NotFound();
        }

        return View(funcionariocargo);
    }

    // POST: FUNCIONARIOCARGOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var funcionariocargo = await _context.FuncionarioCargo.FindAsync(id);
        if (funcionariocargo != null)
        {
            _context.FuncionarioCargo.Remove(funcionariocargo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FuncionarioCargoExists(int? id)
    {
        return _context.FuncionarioCargo.Any(e => e.Id == id);
    }
}
