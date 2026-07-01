using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarCRM.Data;
using CarCRM.Models;

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
                .Include(v => v.VeiculoCor);
            return View(await carCRMContext.ToListAsync());
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
                .Include(v => v.VeiculoTipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.VeiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.VeiculosTipo = _context.VeiculoTipos.ToList();
            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            var veiculo = new Veiculo();
            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.VeiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.VeiculosTipo = _context.VeiculoTipos.ToList();

            //ViewData["VeiculoMarcaId"] = new SelectList(_context.veiculoMarcas, "Id", "Id");
            //ViewData["VeiculoTipoId"] = new SelectList(_context.VeiculoTipos, "Id", "Id");
            return View(veiculo);
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int veiculoCorId, int kilometragemAtual, string placa, string combustivel, string motorizacao, int anoFabricacao, int anoModelo,
        int veiculoTipoId, int veiculoMarcaId, bool excluido)
        {
          
            var veiculo = new Veiculo
            {
                VeiculoCorId = veiculoCorId,
                KilometragemAtual = kilometragemAtual,
                Placa = placa,
                Combustivel = combustivel,
                Motorizacao = motorizacao,
                AnoFabricacao = anoFabricacao,
                AnoModelo =  anoModelo,
                VeiculoTipoId = veiculoTipoId,
                VeiculoMarcaId = veiculoMarcaId,
                Excluido = excluido
            };
            
            if (ModelState.IsValid)
            {
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["VeiculoMarcaId"] = new SelectList(_context.veiculoMarcas, "Id", "Id", veiculo.VeiculoMarcaId);
            //ViewData["VeiculoTipoId"] = new SelectList(_context.VeiculoTipos, "Id", "Id", veiculo.VeiculoTipoId);
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            if (veiculo == null)
            {
                return NotFound();
            }
            //ViewData["VeiculoMarcaId"] = new SelectList(_context.veiculoMarcas, "Id", "Id", veiculo.VeiculoMarcaId);
            //ViewData["VeiculoTipoId"] = new SelectList(_context.VeiculoTipos, "Id", "Id", veiculo.VeiculoTipoId);
            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.VeiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.VeiculosTipo = _context.VeiculoTipos.ToList();
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, int veiculoCorId, int kilometragemAtual, string placa, string combustivel, string motorizacao, int anoFabricacao, int anoModelo,
        int veiculoTipoId, int veiculoMarcaId, bool excluido)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (id != veiculo?.Id)
            {
                return NotFound();
            }

            veiculo.VeiculoCorId = veiculoCorId;
            veiculo.KilometragemAtual = kilometragemAtual;
            veiculo.Placa = placa;
            veiculo.Combustivel = combustivel;
            veiculo.Motorizacao = motorizacao;
            veiculo.AnoFabricacao = anoFabricacao;
            veiculo.AnoModelo = anoModelo;
            veiculo.VeiculoTipoId = veiculoTipoId;
            veiculo.VeiculoMarcaId = veiculoMarcaId;
            veiculo.Excluido = excluido;
   
            _context.Update(veiculo);
            await _context.SaveChangesAsync();
              
            return RedirectToAction(nameof(Index));
            
            //ViewData["VeiculoMarcaId"] = new SelectList(_context.veiculoMarcas, "Id", "Id", veiculo.VeiculoMarcaId);
            //ViewData["VeiculoTipoId"] = new SelectList(_context.VeiculoTipos, "Id", "Id", veiculo.VeiculoTipoId);
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
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            ViewBag.veiculosCor = _context.VeiculoCor.ToList();
            ViewBag.VeiculosMarca = _context.veiculoMarcas.ToList();
            ViewBag.VeiculosTipo = _context.VeiculoTipos.ToList();
            return View(veiculo);
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
