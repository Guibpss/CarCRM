
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc.Formatters;

public class VendasController : Controller
{
    private readonly CarCRMContext _context;

    public VendasController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: VENDAS
    public async Task<IActionResult> Index()    
    {
        var carCRMContext = _context.Vendas
            .Include(v => v.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda);
        var vendas = await carCRMContext.ToListAsync();
        var vendasViewModel = vendas.Select(v => 
            new VendaViewModel
            {
                Id = v.Id,
                CriadoEm = v.CriadoEm,
                Excluido = v.Excluido,
                DataVenda = v.DataVenda,
                Desconto = v.Desconto,
                ClienteId = v.ClienteId,
                Cliente = new ClienteViewModel
                {
                    Id = v.Cliente.Id,
                    PessoaId = v.Cliente.PessoaId,
                    Pessoa = v.Cliente.Pessoa is PessoaFisica
                        ? new PessoaFisicaViewModel
                        {
                            Id = v.Cliente.Pessoa.Id,
                            Nome = v.Cliente.Pessoa.Nome,
                            Email = v.Cliente.Pessoa.Email
                        }
                        : (PessoaViewModel)new PessoaJuridicaViewModel
                        {
                            Id = v.Cliente.Pessoa.Id,
                            Nome = v.Cliente.Pessoa.Nome,
                            Email = v.Cliente.Pessoa.Email
                        }
                },
                VendedorId = v.VendedorId,
                Vendedor = new UsuarioViewModel
                {
                    Id = v.Vendedor.Id,
                    Nome = v.Vendedor.Nome
                },
                StatusVendaId = v.StatusVenda.Id,
                StatusVenda = new StatusVendaViewModel
                {
                    Id = v.StatusVenda.Id,
                    Nome = v.StatusVenda.Nome
                }
            }).ToList();

        return View(vendasViewModel);
    }

    // GET: VENDAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
            .Include(v => v.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        var vendaViewModel = new VendaViewModel
        {
            Id = venda.Id,
            CriadoEm = venda.CriadoEm,
            Excluido = venda.Excluido,
            DataVenda = venda.DataVenda,
            ValorVenda = venda.ValorVenda,
            Desconto = venda.Desconto,
            ClienteId = venda.ClienteId,
            Cliente = new ClienteViewModel
            {
                Id = venda.Cliente.Id,
                PessoaId = venda.Cliente.PessoaId,
                Pessoa = venda.Cliente.Pessoa is PessoaFisica
                    ? new PessoaFisicaViewModel
                    {
                        Id = venda.Cliente.Pessoa.Id,
                        Nome = venda.Cliente.Pessoa.Nome,
                        Email = venda.Cliente.Pessoa.Email
                    }
                    : (PessoaViewModel)new PessoaJuridicaViewModel
                    {
                        Id = venda.Cliente.Pessoa.Id,
                        Nome = venda.Cliente.Pessoa.Nome,
                        Email = venda.Cliente.Pessoa.Email
                    }
            },
            VendedorId = venda.VendedorId,
            Vendedor = new UsuarioViewModel
            {
                Id = venda.Vendedor.Id,
                Nome = venda.Vendedor.Nome
            },
            StatusVendaId = venda.StatusVendaId,
            StatusVenda = new StatusVendaViewModel
            {
                Id = venda.StatusVenda.Id,
                Nome = venda.StatusVenda.Nome
            }
        };

        return View(vendaViewModel);
    }

    // GET: VENDAS/Create
    public IActionResult Create()
    {
        CarregarDropdowns();
        return View(new VendaViewModel());
    }

    // POST: VENDAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VendaViewModel vendaViewModel)
    {

            var venda = new Venda
            {
                DataVenda = vendaViewModel.DataVenda,
                ValorVenda = vendaViewModel.ValorVenda,
                Desconto = vendaViewModel.Desconto,
                Excluido = vendaViewModel.Excluido,
                ClienteId = vendaViewModel.ClienteId,
                VendedorId = vendaViewModel.VendedorId,
                StatusVendaId = vendaViewModel.StatusVendaId
            };

        if (ModelState.IsValid)
        {
            _context.Add(venda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
            
        CarregarDropdowns();
        return View(vendaViewModel);
    }

    // GET: VENDAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
                .Include(v => v.Cliente)
                    .ThenInclude(c => c.Pessoa)
                .Include(v => v.Vendedor)
                .Include(v => v.StatusVenda)
                .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        CarregarDropdowns();

        var vendaViewModel = new VendaViewModel
        {
            DataVenda = venda.DataVenda,
            ValorVenda = venda.ValorVenda,
            Desconto = venda.Desconto,
            Excluido = venda.Excluido,
            CriadoEm = venda.CriadoEm,
            ClienteId = venda.ClienteId,
            Cliente = new ClienteViewModel
            {
                Id = venda.Cliente.Id,
                PessoaId = venda.Cliente.PessoaId,
                Pessoa = venda.Cliente.Pessoa is PessoaFisica
                    ? new PessoaFisicaViewModel
                    {
                        Id = venda.Cliente.Pessoa.Id,
                        Nome = venda.Cliente.Pessoa.Nome,
                        Email = venda.Cliente.Pessoa.Email
                    }
                    : (PessoaViewModel)new PessoaJuridicaViewModel
                    {
                        Id = venda.Cliente.Pessoa.Id,
                        Nome = venda.Cliente.Pessoa.Nome,
                        Email = venda.Cliente.Pessoa.Email
                    }

            },

            VendedorId = venda.VendedorId,
            Vendedor = new UsuarioViewModel
            {
                Id = venda.Vendedor.Id,
                Nome = venda.Vendedor.Nome
            },
            StatusVendaId = venda.StatusVendaId,
            StatusVenda = new StatusVendaViewModel
            {
                Id = venda.StatusVenda.Id,
                Nome = venda.StatusVenda.Nome
            }
        };
        return View(vendaViewModel);
    }

    // POST: VENDAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, VendaViewModel vendaViewModel)
    {
        if (id != vendaViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var venda = await _context.Vendas.FindAsync(id);

                if (venda == null)
                {
                    return NotFound();
                }

                venda.DataVenda = vendaViewModel.DataVenda;
                venda.ValorVenda = vendaViewModel.ValorVenda;
                venda.Desconto = vendaViewModel.Desconto;
                venda.ClienteId = vendaViewModel.ClienteId;
                venda.VendedorId = vendaViewModel.VendedorId;
                venda.StatusVendaId = vendaViewModel.StatusVendaId;

                _context.Update(venda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(vendaViewModel.Id)) return NotFound();
                else throw;
            }
        }
        CarregarDropdowns();
        return View(vendaViewModel);
    }

    // GET: VENDAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var venda = await _context.Vendas
            .Include(v => v.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(v => v.Vendedor)
            .Include(v => v.StatusVenda)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        var vendaViewModel = new VendaViewModel
        {
            DataVenda = venda.DataVenda,
            ValorVenda = venda.ValorVenda,
            Desconto = venda.Desconto,
            Excluido = venda.Excluido,
            CriadoEm = venda.CriadoEm,
            ClienteId = venda.ClienteId,
            Cliente = new ClienteViewModel
            {
                Id = venda.Cliente.Id,
                PessoaId = venda.Cliente.PessoaId,
                Pessoa = venda.Cliente.Pessoa is PessoaFisica
                   ? new PessoaFisicaViewModel
                   {
                       Id = venda.Cliente.Pessoa.Id,
                       Nome = venda.Cliente.Pessoa.Nome,
                       Email = venda.Cliente.Pessoa.Email
                   }
                   : (PessoaViewModel)new PessoaJuridicaViewModel
                   {
                       Id = venda.Cliente.Pessoa.Id,
                       Nome = venda.Cliente.Pessoa.Nome,
                       Email = venda.Cliente.Pessoa.Email
                   }

            },

            VendedorId = venda.VendedorId,
            Vendedor = new UsuarioViewModel
            {
                Id = venda.Vendedor.Id,
                Nome = venda.Vendedor.Nome
            },
            StatusVendaId = venda.StatusVendaId,
            StatusVenda = new StatusVendaViewModel
            {
                Id = venda.StatusVenda.Id,
                Nome = venda.StatusVenda.Nome
            }
        };

        return View(vendaViewModel);
    }

    // POST: VENDAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda != null)
        {
            _context.Vendas.Remove(venda);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VendaExists(int? id)
    {
        return _context.Vendas.Any(e => e.Id == id);
    }

    private void CarregarDropdowns()
    {
        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(p => p.Pessoa.Nome).ToList();
        ViewBag.vendedores = _context.Usuarios.OrderBy(p => p.Nome).ToList();
        ViewBag.StatusVendas = _context.StatusVendas.OrderBy(p => p.Nome).ToList();
    }
}
