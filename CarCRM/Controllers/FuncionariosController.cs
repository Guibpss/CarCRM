
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

public class FuncionariosController : Controller
{
    private readonly CarCRMContext _context;

    public FuncionariosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: FUNCIONARIOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Funcionarios.ToListAsync());
    }

    // GET: FUNCIONARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _context.Funcionarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (funcionario == null)
        {
            return NotFound();
        }

        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FUNCIONARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Admissão,CriadoEm,Salario,UsuarioId,CargoId")] Funcionario funcionario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null)
        {
            return NotFound();
        }
        return View(funcionario);
    }

    // POST: FUNCIONARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Admissão,CriadoEm,Salario,UsuarioId,CargoId")] Funcionario funcionario)
    {
        if (id != funcionario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(funcionario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.Id))
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
        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _context.Funcionarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (funcionario == null)
        {
            return NotFound();
        }

        return View(funcionario);
    }

    // POST: FUNCIONARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario != null)
        {
            _context.Funcionarios.Remove(funcionario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FuncionarioExists(int? id)
    {
        return _context.Funcionarios.Any(e => e.Id == id);
    }
}
