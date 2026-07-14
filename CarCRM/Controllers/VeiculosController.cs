using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;

namespace CarCRM.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly CarCRMContext _context;

        public VeiculosController(CarCRMContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            var carCRMContext = _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao);
            var veiculos = await carCRMContext.ToListAsync();

            var veiculosViewModel = veiculos.Select(v => 
            new VeiculoViewModel 
            {
                Id = v.Id,
                AnoFabricacao = v.AnoFabricacao,
                AnoModelo = v.AnoModelo,
                VeiculoCor = new VeiculoCorViewModel
                {
                    Nome = v.VeiculoCor.Nome
                },
                VeiculoMarca = new VeiculoMarcaViewModel
                {
                    Id = v.VeiculoMarca.Id,
                    Nome = v.VeiculoMarca.Nome
                },
                KilometragemAtual = v.KilometragemAtual,
                Placa = v.Placa,
                VeiculoCombustivel = new VeiculoCombustivelViewModel
                {
                    Nome = v.VeiculoCombustivel.Nome
                },
                VeiculoMotorizacao = new VeiculoMotorizacaoViewModel
                {
                    Nome = v.VeiculoMotorizacao.Nome
                },
                VeiculoTipo = new VeiculoTipoViewModel
                {
                    Nome = v.VeiculoTipo.Nome
                }

            }).ToList();

            return View(veiculosViewModel);
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var veiculoViewModel = new VeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                KilometragemAtual = veiculo.KilometragemAtual,
                VeiculoCombustivel = new VeiculoCombustivelViewModel
                {
                    Id = veiculo.VeiculoCombustivel.Id,
                    Nome = veiculo.VeiculoCombustivel.Nome
                },
                VeiculoMotorizacao = new VeiculoMotorizacaoViewModel
                {
                    Id = veiculo.VeiculoMotorizacao.Id,
                    Nome = veiculo.VeiculoMotorizacao.Nome
                },
                VeiculoCor = new VeiculoCorViewModel
                {
                    Id = veiculo.VeiculoCor.Id,
                    Nome = veiculo.VeiculoCor.Nome
                },
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                VeiculoTipo = new VeiculoTipoViewModel
                {
                    Id = veiculo.VeiculoTipo.Id,
                    Nome = veiculo.VeiculoTipo.Nome
                },
                VeiculoMarca = new VeiculoMarcaViewModel
                {
                    Id = veiculo.VeiculoMarca.Id,
                    Nome = veiculo.VeiculoMarca.Nome
                },
                VeiculoModelo = new VeiculoModeloViewModel
                {
                    Id = veiculo.VeiculoModelo.Id,
                    Nome = veiculo.VeiculoModelo.Nome
                },
                CriadoEm = veiculo.CriadoEm,
                Excluido = veiculo.Excluido,


            };

            return View(veiculoViewModel);

        }

        [HttpGet]
        public JsonResult ModelosPorMarca(int marcaId)
        {
            var modelos = _context.VeiculoModelo
                .Where(m => m.VeiculoMarcaId == marcaId)
                .Select(m => new { m.Id, m.Nome })
                .ToList();

            return Json(modelos);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            var veiculo = new Veiculo();
            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.ToList();

            //ViewData["VeiculoMarcaId"] = new SelectList(_context.veiculoMarcas, "Id", "Id");
            //ViewData["VeiculoTipoId"] = new SelectList(_context.VeiculoTipos, "Id", "Id");
            return View(new VeiculoViewModel());
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoViewModel veiculoViewModel)
        {
          
            var veiculo = new Veiculo
            {
                VeiculoCorId = veiculoViewModel.VeiculoCorId,
                KilometragemAtual = veiculoViewModel.KilometragemAtual,
                Placa = veiculoViewModel.Placa,
                VeiculoCombustivelId = veiculoViewModel.VeiculoCombustivelId,
                VeiculoMotorizacaoId = veiculoViewModel.VeiculoMotorizacaoId,
                AnoFabricacao = veiculoViewModel.AnoFabricacao,
                AnoModelo = veiculoViewModel.AnoModelo,
                VeiculoTipoId = veiculoViewModel.VeiculoTipoId,
                VeiculoMarcaId = veiculoViewModel.VeiculoMarcaId,
                VeiculoModeloId = veiculoViewModel.VeiculoModeloId,
                Excluido = veiculoViewModel.Excluido
            };
            
            if (ModelState.IsValid)
            {
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.ToList();


            return View(veiculoViewModel);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            if (veiculo == null)
            {
                return NotFound();
            }

            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.ToList();

            var veiculoViewModel = new VeiculoViewModel 
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                KilometragemAtual = veiculo.KilometragemAtual,
                VeiculoCombustivel = new VeiculoCombustivelViewModel
                {
                    Id = veiculo.VeiculoCombustivel.Id,
                    Nome = veiculo.VeiculoCombustivel.Nome
                },
                VeiculoMotorizacao = new VeiculoMotorizacaoViewModel
                {
                    Id = veiculo.VeiculoMotorizacao.Id,
                    Nome = veiculo.VeiculoMotorizacao.Nome
                },
                VeiculoCor = new VeiculoCorViewModel
                {
                    Id = veiculo.VeiculoCor.Id,
                    Nome = veiculo.VeiculoCor.Nome
                },
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                VeiculoTipo = new VeiculoTipoViewModel
                {
                    Id = veiculo.VeiculoTipo.Id,
                    Nome = veiculo.VeiculoTipo.Nome
                },
                VeiculoMarca = new VeiculoMarcaViewModel
                {
                    Id = veiculo.VeiculoMarca.Id,
                    Nome = veiculo.VeiculoMarca.Nome
                },
                VeiculoModelo = new VeiculoModeloViewModel
                {
                    Id = veiculo.VeiculoModelo.Id,
                    Nome = veiculo.VeiculoModelo.Nome
                }

                //TODO: demais
            };

            return View(veiculoViewModel);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, VeiculoViewModel veiculoViewModel)
        {
            if (id != veiculoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var veiculo = await _context.Veiculos.FindAsync(id);

                    if (veiculo == null)
                    {
                        return NotFound();
                    }

                    veiculo.VeiculoCorId = veiculoViewModel.VeiculoCorId;
                    veiculo.KilometragemAtual = veiculoViewModel.KilometragemAtual;
                    veiculo.Placa = veiculoViewModel.Placa;
                    veiculo.VeiculoCombustivelId = veiculoViewModel.VeiculoCombustivelId;
                    veiculo.VeiculoMotorizacaoId = veiculoViewModel.VeiculoMotorizacaoId;
                    veiculo.AnoFabricacao = veiculoViewModel.AnoFabricacao;
                    veiculo.AnoModelo = veiculoViewModel.AnoModelo;
                    veiculo.VeiculoTipoId = veiculoViewModel.VeiculoTipoId;
                    veiculo.VeiculoMarcaId = veiculoViewModel.VeiculoMarcaId;
                    veiculo.VeiculoModeloId = veiculoViewModel.VeiculoModeloId;
                    veiculo.Excluido = veiculoViewModel.Excluido;

                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculoViewModel.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));

            }
            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.ToList();
            return View(veiculoViewModel);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var veiculoViewModel = new VeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                KilometragemAtual = veiculo.KilometragemAtual,
                VeiculoCombustivel = new VeiculoCombustivelViewModel
                {
                    Id = veiculo.VeiculoCombustivel.Id,
                    Nome = veiculo.VeiculoCombustivel.Nome
                },
                VeiculoMotorizacao = new VeiculoMotorizacaoViewModel
                {
                    Id = veiculo.VeiculoMotorizacao.Id,
                    Nome = veiculo.VeiculoMotorizacao.Nome
                },
                VeiculoCor = new VeiculoCorViewModel
                {
                    Id = veiculo.VeiculoCor.Id,
                    Nome = veiculo.VeiculoCor.Nome
                },
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                VeiculoTipo = new VeiculoTipoViewModel
                {
                    Id = veiculo.VeiculoTipo.Id,
                    Nome = veiculo.VeiculoTipo.Nome
                },
                VeiculoMarca = new VeiculoMarcaViewModel
                {
                    Id = veiculo.VeiculoMarca.Id,
                    Nome = veiculo.VeiculoMarca.Nome
                },
                VeiculoModelo = new VeiculoModeloViewModel
                {
                    Id = veiculo.VeiculoModelo.Id,
                    Nome = veiculo.VeiculoModelo.Nome
                },
                CriadoEm = veiculo.CriadoEm,
                Excluido = veiculo.Excluido,


            };

            return View(veiculoViewModel);

        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculos.Any(e => e.Id == id);
        }
    }
}
