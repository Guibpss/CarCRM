using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
public class ServicosController : Controller
{
    private readonly CarCRMContext _context;

    public ServicosController(CarCRMContext context)
    {  
        _context = context; 
    
    }

    public async Task<IActionResult> Index()
    {
        var servicos = await _context.Servicos
            .Include(s => s.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(s => s.ServicoTipo)
            .ToListAsync();

        var servicosViewModel = servicos.Select(s => new ServicoViewModel
        {
            Id = s.Id,
            Nome = s.Nome,
            Valor = s.Valor,
            ServicoTipoId = s.ServicoTipoId,
            ServicoTipo = new ServicoTipoViewModel
            {
                Id = s.ServicoTipo.Id,
                Nome = s.ServicoTipo.Nome
            },
            ClienteId = s.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = s.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = s.Cliente.Pessoa.Nome
                }
            }
        }).ToList();

        return View(servicosViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos
            .Include(s => s.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(s => s.ServicoTipo)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servico == null)
        {
            return NotFound();
        }

        var servicoViewModel = new ServicoViewModel
        {
            Id = servico.Id,
            Nome = servico.Nome,
            Valor = servico.Valor,
            ServicoTipoId = servico.ServicoTipoId,
            ServicoTipo = new ServicoTipoViewModel
            {
                Id = servico.ServicoTipo.Id,
                Nome = servico.ServicoTipo.Nome
            },
            ClienteId = servico.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = servico.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = servico.Cliente.Pessoa.Nome
                }
            }
        };

        return View(servicoViewModel);
    }

    //GET/CREATE
    public IActionResult Create()
    {
        var servicoViewModel = new ServicoViewModel();
        var servicoTipos = _context.ServicoTipos.ToList();
        var clientes = _context.Clientes.Include(c => c.Pessoa).ToList();
        ViewBag.Clientes = clientes;
        ViewBag.ServicoTipos = servicoTipos;
        return View(servicoViewModel);
    }

    //POST/CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServicoViewModel servicoViewModel)
    {
        if (ModelState.IsValid)
        {
            var servico = new Servico
            {
                Nome = servicoViewModel.Nome,
                Valor = servicoViewModel.Valor,
                ServicoTipoId = servicoViewModel.ServicoTipoId,
                ClienteId = servicoViewModel.ClienteId,
            };

            _context.Add(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ServicoTipos = _context.ServicoTipos.ToList();
        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).ToList();
        return View(servicoViewModel);
    }


    //GET/EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos
            .Include(s => s.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(s => s.ServicoTipo)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servico == null)
        {
            return NotFound();
        }

        ViewBag.ServicoTipos = _context.ServicoTipos.ToList();
        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).ToList();

        var servicoViewModel = new ServicoViewModel
        {
            Id = servico.Id,
            Nome = servico.Nome,
            Valor = servico.Valor,
            ServicoTipoId = servico.ServicoTipoId,
            ClienteId = servico.ClienteId,
            
        };
        return View(servicoViewModel);
    }


    //POST/EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ServicoViewModel servicoViewModel)
    {
        if (id != servicoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var servico = await _context.Servicos
                    .Include(s => s.Cliente)
                        .ThenInclude(c => c.Pessoa)
                    .Include(s => s.ServicoTipo)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (servico == null)
                {
                    return NotFound();
                }

                servico.Nome = servicoViewModel.Nome;
                servico.Valor = servicoViewModel.Valor;
                servico.ServicoTipoId = servicoViewModel.ServicoTipoId;
                servico.ClienteId = servicoViewModel.ClienteId;

                _context.Update(servico);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(servicoViewModel.Id))
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
        ViewBag.ServicoTipos = _context.ServicoTipos.ToList();
        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).ToList();
        return View(servicoViewModel);
    }


    //GET/DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos
            .Include(s => s.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(s => s.ServicoTipo)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servico == null)
        {
            return NotFound();
        }

        var servicoViewModel = new ServicoViewModel
        {
            Id = servico.Id,
            Nome = servico.Nome,
            Valor = servico.Valor,
            ServicoTipoId = servico.ServicoTipoId,
            ServicoTipo = new ServicoTipoViewModel
            {
                Id = servico.ServicoTipo.Id,
                Nome = servico.ServicoTipo.Nome
            },
            ClienteId = servico.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = servico.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = servico.Cliente.Pessoa.Nome
                }
            }
        };

        return View(servicoViewModel);
    }

    //POST/DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var servico = await _context.Servicos.FindAsync(id);
        if (servico != null)
        {
            _context.Servicos.Remove(servico);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ServicoExists(int? id)
    {
        return _context.Servicos.Any(s => s.Id == id);
    }

}

