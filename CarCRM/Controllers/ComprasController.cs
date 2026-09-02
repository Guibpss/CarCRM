using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CarCRM.Controllers
{
    public class ComprasController : Controller
    {
        private readonly CarCRMContext _context;

        public ComprasController(CarCRMContext context)
        {
            _context = context;
        }

        // GET: COMPRAS
        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                .Include(c => c.Vendedor)
                .Include(c => c.Veiculo)
                .Include(c => c.StatusCompra)
                .ToListAsync();

            var comprasViewModel = compras.Select(c => new CompraViewModel
            {
                Id = c.Id,
                CriadoEm = c.CriadoEm,
                Excluido = c.Excluido,
                DataCompra = c.DataCompra,
                ValorCompra = c.ValorCompra,
                Desconto = c.Desconto,
                VendedorId = c.VendedorId,
                Vendedor = new UsuarioViewModel
                {
                    Id = c.Vendedor.Id,
                    Nome = c.Vendedor.Nome,
                },
                VeiculoId = c.VeiculoId,
                Veiculo = new VeiculoViewModel
                {
                    Id = c.Veiculo.Id,
                    Placa = c.Veiculo.Placa,
                },
                StatusCompraId = c.StatusCompraId,
                StatusCompra = new StatusCompraViewModel
                {
                    Id= c.StatusCompra.Id,
                    Nome = c.StatusCompra.Nome,
                }
            }).ToList();

            return View(comprasViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Vendedor)
                .Include(c => c.Veiculo)
                .Include(c => c.StatusCompra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
            {
                return NotFound();
            }

            var compraViewModel = new CompraViewModel
            {
                Id = compra.Id,
                CriadoEm = compra.CriadoEm,
                Excluido = compra.Excluido,
                DataCompra = compra.DataCompra,
                ValorCompra = compra.ValorCompra,
                Desconto = compra.Desconto,
                VendedorId = compra.Vendedor.Id,
                Vendedor = new UsuarioViewModel
                {
                    Id = compra.Vendedor.Id,
                    Nome = compra.Vendedor.Nome,
                },
                VeiculoId = compra.VeiculoId,
                Veiculo = new VeiculoViewModel
                {
                    Id = compra.Veiculo.Id,
                    Placa = compra.Veiculo.Placa,
                },
                StatusCompraId = compra.StatusCompraId,
                StatusCompra = new StatusCompraViewModel
                {
                    Id = compra.StatusCompra.Id,
                    Nome = compra.StatusCompra.Nome,
                }
            };

            return View(compraViewModel);
        }

        public IActionResult Create()
        {
            CarregarDropdowns();
            return View(new CompraViewModel());
        }

        // POST: COMPRAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompraViewModel compraViewModel)
        {

            var compra = new Compra
            {
                DataCompra = compraViewModel.DataCompra,
                ValorCompra = compraViewModel.ValorCompra,
                Desconto = compraViewModel.Desconto,
                Excluido = compraViewModel.Excluido,
                VendedorId = compraViewModel.VendedorId,
                VeiculoId = compraViewModel.VeiculoId,
                StatusCompraId = compraViewModel.StatusCompraId
            };

            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CarregarDropdowns();
            return View(compraViewModel);
        }

        // GET: COMPRAS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Vendedor)
                .Include(c => c.Veiculo)
                .Include(c => c.StatusCompra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
            {
                return NotFound();
            }

            CarregarDropdowns();

            var compraViewModel = new CompraViewModel
            {
                Id = compra.Id,
                DataCompra = compra.DataCompra,
                ValorCompra = compra.ValorCompra,
                Desconto = compra.Desconto,
                Excluido = compra.Excluido,
                CriadoEm = compra.CriadoEm,
                VendedorId = compra.VendedorId,
                Vendedor = new UsuarioViewModel
                {
                    Id = compra.Vendedor.Id,
                    Nome = compra.Vendedor.Nome,
                },
                VeiculoId = compra.VeiculoId,
                Veiculo = new VeiculoViewModel
                {
                    Id = compra.Veiculo.Id,
                    Placa = compra.Veiculo.Placa,
                },
                StatusCompraId = compra.StatusCompraId,
                StatusCompra = new StatusCompraViewModel
                {
                    Id = compra.StatusCompra.Id,
                    Nome = compra.StatusCompra.Nome
                }
            };

            return View(compraViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CompraViewModel compraViewModel)
        {
            if (id != compraViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var compra = await _context.Compras.FindAsync(id);

                    if (compra == null)
                    {
                        return NotFound();
                    }

                    compra.DataCompra = compraViewModel.DataCompra;
                    compra.ValorCompra = compraViewModel.ValorCompra;
                    compra.Desconto = compraViewModel.Desconto;
                    compra.VendedorId = compraViewModel.VendedorId;
                    compra.VeiculoId = compraViewModel.VeiculoId;
                    compra.StatusCompraId = compraViewModel.StatusCompraId;

                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!CompraExists(compraViewModel.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            CarregarDropdowns();
            return View(compraViewModel);
        }

        // GET: COMPRAS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Vendedor)
                .Include(c => c.Veiculo)
                .Include(c => c.StatusCompra)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
            {
                return NotFound();
            }

            var compraViewModel = new CompraViewModel
            {
                DataCompra = compra.DataCompra,
                ValorCompra = compra.ValorCompra,
                Desconto = compra.Desconto,
                Excluido = compra.Excluido,
                CriadoEm = compra.CriadoEm,
                VendedorId = compra.VendedorId,
                Vendedor = new UsuarioViewModel
                {
                    Id = compra.Vendedor.Id,
                    Nome = compra.Vendedor.Nome,
                },
                VeiculoId = compra.VeiculoId,
                Veiculo = new VeiculoViewModel
                {
                    Id = compra.Veiculo.Id,
                    Placa = compra.Veiculo.Placa,
                },
                StatusCompraId = compra.StatusCompraId,
                StatusCompra = new StatusCompraViewModel
                {
                    Id = compra.StatusCompra.Id,
                    Nome = compra.StatusCompra.Nome
                }
            };

            return View(compraViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int? id)
        {
            return _context.Compras.Any(c => c.Id == id);
        }
        

        private void CarregarDropdowns()
        {
            ViewBag.vendedores = _context.Usuarios.OrderBy(c => c.Nome).ToList();
            ViewBag.veiculos = _context.Veiculos.ToList();
            ViewBag.statuscompras = _context.StatusCompras.OrderBy(c => c.Nome).ToList();
        }
    }
}
