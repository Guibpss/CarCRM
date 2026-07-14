using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;

namespace CarCRM.Controllers
{
    public class EstoquesController : Controller
    {
        private readonly CarCRMContext _context;

        public EstoquesController(CarCRMContext context)
        {
            _context = context;
        }

        // GET: Estoques
        public async Task<IActionResult> Index()
        {
            var estoques = await _context.Estoques.ToListAsync();

            var estoquesViewModel = estoques.Select(e =>
            new EstoqueViewModel
            {
                Id = e.Id,
                Nome = e.Nome,
                DataEntrada = e.DataEntrada,
                CriadoEm = e.CriadoEm,
                Excluido = e.Excluido
            }).ToList();

            return View(estoquesViewModel);
        }

        // GET: Estoques/Details/5
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

            var estoqueViewModel = new EstoqueViewModel
            {
                Id = estoque.Id,
                Nome = estoque.Nome,
                DataEntrada = estoque.DataEntrada,
                CriadoEm = estoque.CriadoEm,
                Excluido = estoque.Excluido
            };

            return View(estoqueViewModel);
        }

        // GET: Estoques/Create
        public IActionResult Create()
        {
            return View(new EstoqueViewModel());
        }

        // POST: Estoques/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstoqueViewModel estoqueViewModel)
        {
            var estoque = new Estoque
            {
                Nome = estoqueViewModel.Nome,
                DataEntrada = estoqueViewModel.DataEntrada,
                CriadoEm = DateTime.Now,
                Excluido = estoqueViewModel.Excluido
            };

            if (ModelState.IsValid)
            {
                _context.Add(estoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(estoqueViewModel);
        }

        // GET: Estoques/Edit/5
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

            var estoqueViewModel = new EstoqueViewModel
            {
                Id = estoque.Id,
                Nome = estoque.Nome,
                DataEntrada = estoque.DataEntrada,
                CriadoEm = estoque.CriadoEm,
                Excluido = estoque.Excluido
            };

            return View(estoqueViewModel);
        }

        // POST: Estoques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EstoqueViewModel estoqueViewModel)
        {
            if (id != estoqueViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var estoque = await _context.Estoques.FindAsync(id);

                    if (estoque == null)
                    {
                        return NotFound();
                    }

                    estoque.Nome = estoqueViewModel.Nome;
                    estoque.DataEntrada = estoqueViewModel.DataEntrada;
                    estoque.Excluido = estoqueViewModel.Excluido;

                    _context.Update(estoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstoqueExists(estoqueViewModel.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(estoqueViewModel);
        }

        // GET: Estoques/Delete/5
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

            var estoqueViewModel = new EstoqueViewModel
            {
                Id = estoque.Id,
                Nome = estoque.Nome,
                DataEntrada = estoque.DataEntrada,
                CriadoEm = estoque.CriadoEm,
                Excluido = estoque.Excluido
            };

            return View(estoqueViewModel);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque != null)
            {
                _context.Estoques.Remove(estoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstoqueExists(int id)
        {
            return _context.Estoques.Any(e => e.Id == id);
        }
    }
}
