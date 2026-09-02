using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

public class PagamentoController : Controller
{
    private readonly CarCRMContext _context;

    public PagamentoController(CarCRMContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> Index()
    {
        var pagamentos = await _context.Pagamentos
            .Include(p => p.StatusPagamento)
            .Include(p => p.MetodoPagamento)
            .ToListAsync();

        var pagamentoViewModel = pagamentos.Select(p => new PagamentoViewModel
        {
            Id = p.Id,
            Valor = p.Valor,
            Parcelas = p.Parcelas,
            DataVencimento = p.DataVencimento,
            DataPagamento = p.DataPagamento,
            StatusPagamentoId = p.StatusPagamentoId,
            StatusPagamento = new StatusPagamentoViewModel
            {
                Id = p.StatusPagamento.Id,
                Nome = p.StatusPagamento.Nome
            },
            MetodoPagamentoId = p.MetodoPagamentoId,
            MetodoPagamento = new MetodoPagamentoViewModel
            {
                Id = p.MetodoPagamento.Id,
                Nome = p.MetodoPagamento.Nome,
                Ativo = p.MetodoPagamento.Ativo
            }
        }).ToList();

        return View(pagamentoViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos
            .Include(p => p.StatusPagamento)
            .Include(p => p.MetodoPagamento)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pagamento == null)
        {
            return NotFound();
        }

        var pagamentoViewModel = new PagamentoViewModel
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor,
            Parcelas = pagamento.Parcelas,
            DataVencimento = pagamento.DataVencimento,
            DataPagamento = pagamento.DataPagamento,
            StatusPagamentoId = pagamento.StatusPagamentoId,
            StatusPagamento = new StatusPagamentoViewModel
            {
                Id = pagamento.StatusPagamento.Id,
                Nome = pagamento.StatusPagamento.Nome
            },
            MetodoPagamentoId = pagamento.MetodoPagamentoId,
            MetodoPagamento = new MetodoPagamentoViewModel
            {
                Id = pagamento.MetodoPagamento.Id,
                Nome = pagamento.MetodoPagamento.Nome,
                Ativo = pagamento.MetodoPagamento.Ativo
            }
        };

        return View(pagamentoViewModel);
    }

    public IActionResult Create()
    {
        var pagamentoViewModel = new PagamentoViewModel();
        var statusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
        var metodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();
        ViewBag.StatusPagamento = statusPagamento;
        ViewBag.MetodoPagamento = metodoPagamento;
        return View(pagamentoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PagamentoViewModel pagamentoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pagamento = new Pagamento
            {

                Valor = pagamentoViewModel.Valor,
                Parcelas = pagamentoViewModel.Parcelas,
                DataVencimento = pagamentoViewModel.DataVencimento,
                DataPagamento = pagamentoViewModel.DataPagamento,
                StatusPagamentoId = pagamentoViewModel.StatusPagamentoId,
                MetodoPagamentoId = pagamentoViewModel.MetodoPagamentoId
            };

            _context.Add(pagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.StatusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
        ViewBag.MetodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();
        return View(pagamentoViewModel);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos
            .Include(p => p.StatusPagamento)
            .Include(p => p.MetodoPagamento)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pagamento == null)
        {
            return NotFound();
        }

        ViewBag.StatusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
        ViewBag.MetodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();

        var pagamentoViewModel = new PagamentoViewModel
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor,
            Parcelas = pagamento.Parcelas,
            DataVencimento = pagamento.DataVencimento,
            DataPagamento = pagamento.DataPagamento,
            StatusPagamentoId = pagamento.StatusPagamentoId,
            MetodoPagamentoId = pagamento.MetodoPagamentoId
        };

        return View(pagamentoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, PagamentoViewModel pagamentoViewModel)
    {
        if (id != pagamentoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var pagamento = await _context.Pagamentos
                    .Include(p => p.StatusPagamento)
                    .Include(p => p.MetodoPagamento)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pagamento == null)
                {
                    return NotFound();
                }

                pagamento.Valor = pagamentoViewModel.Valor;
                pagamento.Parcelas = pagamentoViewModel.Parcelas;
                pagamento.DataVencimento = pagamentoViewModel.DataVencimento;
                pagamento.DataPagamento = pagamentoViewModel.DataPagamento;
                pagamento.StatusPagamentoId = pagamentoViewModel.StatusPagamentoId;
                pagamento.MetodoPagamentoId = pagamentoViewModel.MetodoPagamentoId;

                _context.Update(pagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!PagamentoExists(pagamentoViewModel.Id))
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
        ViewBag.StatusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
        ViewBag.MetodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();
        return View(pagamentoViewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pagamento = await _context.Pagamentos
            .Include(p => p.StatusPagamento)
            .Include(p => p.MetodoPagamento)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pagamento == null)
        {
            return NotFound();
        }

        var pagamentoViewModel = new PagamentoViewModel
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor,
            Parcelas = pagamento.Parcelas,
            DataPagamento = pagamento.DataPagamento,
            DataVencimento = pagamento.DataVencimento,
            StatusPagamentoId = pagamento.StatusPagamentoId,
            StatusPagamento = new StatusPagamentoViewModel
            {
                Id = pagamento.StatusPagamento.Id,
                Nome = pagamento.StatusPagamento.Nome,
            },
            MetodoPagamentoId = pagamento.MetodoPagamentoId,
            MetodoPagamento = new MetodoPagamentoViewModel
            {
                Id = pagamento.MetodoPagamento.Id,
                Nome = pagamento.MetodoPagamento.Nome,
                Ativo = pagamento.MetodoPagamento.Ativo
            }
        };

        return View(pagamentoViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pagamento = await _context.Pagamentos.FindAsync(id);
        if (pagamento != null)
        {
            _context.Pagamentos.Remove(pagamento);

        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PagamentoExists(int? id)
    {
        return _context.Pagamentos.Any(p => p.Id == id);
    }
}