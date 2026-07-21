
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;

public class PerfilsController : Controller
{
    private readonly CarCRMContext _context;

    public PerfilsController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: PERFILS
    public async Task<IActionResult> Index()    
    {
        var perfis = await _context.Perfis.ToListAsync();
        var perfilViewModel = perfis.Select(p => new PerfilViewModel
        {   
            Id = p.Id,
            Nome = p.Nome
        }).ToList();
        return View(perfilViewModel);
    }

    // GET: PERFILS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        var PerfilViewModel = new PerfilViewModel
        {
            Id = perfil.Id,
            Nome = perfil.Nome
        };

        return View(PerfilViewModel);
    }

    // GET: PERFILS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PERFILS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PerfilViewModel perfilViewModel)
    {
        if (ModelState.IsValid)
        {
            var perfil = new Perfil
            {
                Nome = perfilViewModel.Nome,
            };

            _context.Add(perfil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(perfilViewModel);
    }

    // GET: PERFILS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil == null)
        {
            return NotFound();
        }

        var perfilViewModel = new PerfilViewModel
        {
            Id = perfil.Id,
            Nome = perfil.Nome
        };
        return View(perfilViewModel);
    }

    // POST: PERFILS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, PerfilViewModel perfilViewModel)
    {
        if (id != perfilViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var perfil = _context.Perfis.Find(id);
                if (perfil == null)
                {
                    return NotFound();
                }
                perfil.Nome = perfilViewModel.Nome;
                _context.Update(perfil);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(perfilViewModel.Id))
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
        return View(perfilViewModel);
    }

    // GET: PERFILS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        var perfilViewModel = new PerfilViewModel
        {
            Id = perfil.Id,
            Nome = perfil.Nome
        };

        return View(perfilViewModel);
    }

    // POST: PERFILS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var perfil = await _context.Perfis.FindAsync(id);
        bool hasUsuarios = await _context.Usuarios.AnyAsync(u => u.PerfilId == id);
        if (hasUsuarios)
        {
            TempData["Erro"] = "Não é possível excluir este perfil porque há usuários vinculados a ele.";
            return RedirectToAction(nameof(Index));
        }

        if (perfil != null)
        {
            _context.Perfis.Remove(perfil);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PerfilExists(int? id)
    {
        return _context.Perfis.Any(e => e.Id == id);
    }
}
