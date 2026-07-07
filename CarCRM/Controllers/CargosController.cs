
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;

public class CargosController : Controller
{
    private readonly CarCRMContext _context;

    public CargosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: CARGOS
    public async Task<IActionResult> Index()    
    {
        var cargos = await _context.Cargos.ToListAsync();
        var cargosViwModel = cargos.Select(c => new CargoViewModel
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList();
        return View(cargosViwModel);
    }

    // GET: CARGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cargo == null)
        {
            return NotFound();
        }

        var cargoViewModel = new CargoViewModel
        {
            Id = cargo.Id,
            Nome = cargo.Nome
        };

        return View(cargoViewModel);
    }

    // GET: CARGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CARGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CargoViewModel cargoViewModel)
    {
        if (ModelState.IsValid)
        {
            var cargo = new Cargo
            { 
                Nome = cargoViewModel.Nome 
            };
            _context.Add(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cargoViewModel);
    }

    // GET: CARGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
        {
            return NotFound();
        }
        var cargoViewModel = new CargoViewModel
        {
            Id = cargo.Id,
            Nome = cargo.Nome
        };
        return View(cargoViewModel);
    }

    // POST: CARGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, CargoViewModel cargoViewModel)
    {
        if (id != cargoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var cargo = _context.Cargos.Find(id);
                if(cargo == null)
                    return NotFound();

                cargo.Nome = cargoViewModel.Nome;
                _context.Update(cargo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(cargoViewModel.Id))
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
        return View(cargoViewModel);
    }

    // GET: CARGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cargo = await _context.Cargos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cargo == null)
        {
            return NotFound();
        }

        var cargoViewModel = new CargoViewModel
        {
            Id = cargo.Id,
            Nome = cargo.Nome
        };
        return View(cargoViewModel);
    }

    // POST: CARGOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo != null)
        {
            _context.Cargos.Remove(cargo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CargoExists(int? id)
    {
        return _context.Cargos.Any(e => e.Id == id);
    }
}
