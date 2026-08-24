
using AspNetCoreGeneratedDocument;
using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ClientesController : Controller
{
    private readonly CarCRMContext _context;

    public ClientesController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: CLIENTES
    public async Task<IActionResult> Index()    
    {
        var carCRMContext = _context.Clientes
            .Include(c => c.Pessoa)
            .ThenInclude(p => p.Telefones)
            .ThenInclude(t => t.TelefoneTipo);
        var clientes = await carCRMContext.ToListAsync();

        var clientesViewModel = clientes.Select(c =>
        new ClienteViewModel
        {
            Id = c.Id,
            CriadoEm = c.CriadoEm,
            Excluido = c.Excluido,
            PessoaId = c.PessoaId,
            Pessoa = c.Pessoa is PessoaFisica
                ? new PessoaFisicaViewModel
                {
                    Id = c.Pessoa.Id,
                    Nome = c.Pessoa.Nome,
                    Email = c.Pessoa.Email,
                    CPF = ((PessoaFisica)c.Pessoa).CPF,
                    RG = ((PessoaFisica)c.Pessoa).RG,
                    DataNascimento = ((PessoaFisica)c.Pessoa).DataNascimento,
                    Telefones = c.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome
                            }
                        }).ToList()
                }
                : (PessoaViewModel)new PessoaJuridicaViewModel
                {
                    Id = c.Pessoa.Id,
                    Nome = c.Pessoa.Nome,
                    Email = c.Pessoa.Email,
                    CNPJ = ((PessoaJuridica)c.Pessoa).CNPJ,
                    RazaoSocial = ((PessoaJuridica)c.Pessoa).RazaoSocial,
                    NomeFantasia = ((PessoaJuridica)c.Pessoa).NomeFantasia,
                    NomeInterno = ((PessoaJuridica)c.Pessoa).NomeInterno,
                    Telefones = c.Pessoa.Telefones.Select(t =>
                    new TelefoneViewModel
                    {
                        Id = t.Id,
                        DDD = t.DDD,
                        Numero = t.Numero,
                        TelefoneTipoId = t.TelefoneTipoId,
                        TelefoneTipo = new TelefoneTipoViewModel
                        {
                            Id = t.TelefoneTipo.Id,
                            Nome = t.TelefoneTipo.Nome
                        }
                    }).ToList()
                }
        }).ToList();

        return View(clientesViewModel);
    }

    // GET: CLIENTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes
            .Include(c => c.Pessoa)
            .ThenInclude(p => p.Telefones)
            .ThenInclude(t => t.TelefoneTipo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound();
        }

        var clienteViewModel = new ClienteViewModel
        {
            Id = cliente.Id,
            CriadoEm = cliente.CriadoEm,
            Excluido = cliente.Excluido,
            PessoaId = cliente.Pessoa.Id,
            Pessoa = cliente.Pessoa is PessoaFisica 
                ? new PessoaFisicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CPF = ((PessoaFisica)cliente.Pessoa).CPF,
                    RG = ((PessoaFisica)cliente.Pessoa).RG,
                    DataNascimento = ((PessoaFisica)cliente.Pessoa).DataNascimento,
                    Telefones = cliente.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome
                            }
                        }).ToList()
                }
                : (PessoaViewModel)new PessoaJuridicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CNPJ = ((PessoaJuridica)cliente.Pessoa).CNPJ,
                    RazaoSocial = ((PessoaJuridica)cliente.Pessoa).RazaoSocial,
                    NomeFantasia = ((PessoaJuridica)cliente.Pessoa).NomeFantasia,
                    NomeInterno = ((PessoaJuridica)cliente.Pessoa).NomeInterno,
                    Telefones = cliente.Pessoa.Telefones.Select(t => 
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome
                            }
                        }).ToList()
                }
        };
        ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();
        return View(clienteViewModel);
    }

    // GET: CLIENTES/Create
    public IActionResult Create()
    {
        var clienteViewModel = new ClienteViewModel();
        var telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();
        ViewBag.telefonesTipo = telefonesTipo;
        return View(new ClienteViewModel());
    }

    // POST: CLIENTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClienteViewModel clienteViewModel, string tipoPessoa)
    {
        if (clienteViewModel.Pessoa == null)
        {
            ModelState.AddModelError(string.Empty, "Informe os dados da pessoa.");
            ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();
            return View(clienteViewModel);
        }

        if (!ModelState.IsValid)
        {
            ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();
            return View(clienteViewModel);
        }

        Pessoa pessoa;

        if (tipoPessoa == "pessoaFisica")
        {
            pessoa = new PessoaFisica
            {
                Nome = clienteViewModel.Pessoa.Nome,
                Email = clienteViewModel.Pessoa.Email,
                CPF = clienteViewModel.CPF,
                RG = clienteViewModel.RG,
                DataNascimento = clienteViewModel.DataNascimento
            };
        }
        else
        {
            pessoa = new PessoaJuridica
            {
                Nome = clienteViewModel.Pessoa.Nome,
                Email = clienteViewModel.Pessoa.Email,
                CNPJ = clienteViewModel.CNPJ,
                RazaoSocial = clienteViewModel.RazaoSocial,
                NomeFantasia = clienteViewModel.NomeFantasia,
                NomeInterno = clienteViewModel.NomeInterno,
            };
        }

        var telefoneViewModel = clienteViewModel.Pessoa.Telefones.FirstOrDefault();

        if (telefoneViewModel != null && !string.IsNullOrWhiteSpace(telefoneViewModel.Numero))
        {
            pessoa.Telefones.Add(new Telefone
            {
                DDD = telefoneViewModel.DDD,
                Numero = telefoneViewModel.Numero,
                TelefoneTipoId = telefoneViewModel.TelefoneTipoId
            });
        }

        var cliente = new Cliente
        {
            CriadoEm = DateTime.Now,
            Excluido = clienteViewModel.Excluido,
            Pessoa = pessoa
        };

        _context.Add(cliente);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

// GET: CLIENTES/Edit/5
public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes
            .Include(c => c.Pessoa)
            .ThenInclude(p => p.Telefones)
            .ThenInclude(t => t.TelefoneTipo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound();
        }

        ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();

        var clienteViewModel = new ClienteViewModel
        {
            Id = cliente.Id,
            CriadoEm = cliente.CriadoEm,
            Excluido = cliente.Excluido,
            PessoaId = cliente.PessoaId,
            Pessoa = cliente.Pessoa is PessoaFisica
                ? new PessoaFisicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CPF = ((PessoaFisica)cliente.Pessoa).CPF,
                    RG = ((PessoaFisica)cliente.Pessoa).RG,
                    DataNascimento = ((PessoaFisica)cliente.Pessoa).DataNascimento,
                    Telefones = cliente.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome
                            }
                        }).ToList(),
                }
                : (PessoaViewModel)new PessoaJuridicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CNPJ = ((PessoaJuridica)cliente.Pessoa).CNPJ,
                    RazaoSocial = ((PessoaJuridica)cliente.Pessoa).RazaoSocial,
                    NomeFantasia = ((PessoaJuridica)cliente.Pessoa).RazaoSocial,
                    NomeInterno = ((PessoaJuridica)cliente.Pessoa).NomeInterno,
                    Telefones = cliente.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                        {
                            Id = t.TelefoneTipo.Id,
                            Nome = t.TelefoneTipo.Nome
                        }
                    }).ToList()
                }

        };
        return View(clienteViewModel);
    }

    // POST: CLIENTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ClienteViewModel clienteViewModel, string tipoPessoa)
    {

        if (id != clienteViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var cliente = await _context.Clientes
                    .Include(c => c.Pessoa)
                        .ThenInclude(p => p.Telefones)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Excluido = clienteViewModel.Excluido;
                cliente.Pessoa.Nome = clienteViewModel.Pessoa.Nome;
                cliente.Pessoa.Email = clienteViewModel.Pessoa.Email;

                if (cliente.Pessoa is PessoaFisica pf)
                {
                    pf.CPF = clienteViewModel.CPF;
                    pf.RG = clienteViewModel.RG;
                    pf.DataNascimento = clienteViewModel.DataNascimento;
                }
                else if (cliente.Pessoa is PessoaJuridica pj)
                {
                    pj.CNPJ = clienteViewModel.CNPJ;
                    pj.RazaoSocial = clienteViewModel.RazaoSocial;
                    pj.NomeFantasia = clienteViewModel.NomeFantasia;
                    pj.NomeInterno = clienteViewModel.NomeInterno;
                }

                var telefoneViewModel = clienteViewModel.Pessoa.Telefones.FirstOrDefault();
                var telefone = cliente.Pessoa.Telefones.FirstOrDefault();

                if (telefone != null)
                {
                    telefone.DDD = telefoneViewModel.DDD;
                    telefone.Numero = telefoneViewModel.Numero;
                    telefone.TelefoneTipoId = telefoneViewModel.TelefoneTipoId;
                }
                else if (!string.IsNullOrWhiteSpace(telefoneViewModel?.Numero))
                {
                    cliente.Pessoa.Telefones.Add(new Telefone
                    {
                        DDD = telefoneViewModel.DDD,
                        Numero = telefoneViewModel.Numero,
                        TelefoneTipoId = telefoneViewModel.TelefoneTipoId,
                        PessoaId = cliente.PessoaId
                    });
                }

                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(clienteViewModel.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();
        return View(clienteViewModel);
    }

    // GET: CLIENTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes
            .Include(c => c.Pessoa)
            .ThenInclude(p => p.Telefones)
            .ThenInclude(t => t.TelefoneTipo)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cliente == null)
        {
            return NotFound();
        }

        ViewBag.telefonesTipo = _context.TelefonesTipo.OrderBy(p => p.Nome).ToList();

        var clienteViewModel = new ClienteViewModel
        {
            Id = cliente.Id,
            CriadoEm = cliente.CriadoEm,
            Excluido = cliente.Excluido,
            Pessoa = cliente.Pessoa is PessoaFisica
                ? new PessoaFisicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CPF = ((PessoaFisica)cliente.Pessoa).CPF,
                    RG = ((PessoaFisica)cliente.Pessoa).RG,
                    DataNascimento = ((PessoaFisica)cliente.Pessoa).DataNascimento,
                    Telefones = cliente.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome

                            }
                        }).ToList()
                }
                : (PessoaViewModel)new PessoaJuridicaViewModel
                {
                    Id = cliente.Pessoa.Id,
                    Nome = cliente.Pessoa.Nome,
                    Email = cliente.Pessoa.Email,
                    CNPJ = ((PessoaJuridica)cliente.Pessoa).CNPJ,
                    RazaoSocial = ((PessoaJuridica)cliente.Pessoa).RazaoSocial,
                    NomeFantasia = ((PessoaJuridica)cliente.Pessoa).NomeFantasia,
                    NomeInterno = ((PessoaJuridica)cliente.Pessoa).NomeInterno,
                    Telefones = cliente.Pessoa.Telefones.Select(t =>
                        new TelefoneViewModel
                        {
                            Id = t.Id,
                            DDD = t.DDD,
                            Numero = t.Numero,
                            TelefoneTipoId = t.TelefoneTipoId,
                            TelefoneTipo = new TelefoneTipoViewModel
                            {
                                Id = t.TelefoneTipo.Id,
                                Nome = t.TelefoneTipo.Nome
                            }
                        }).ToList()
                }
        };
        return View(clienteViewModel);
    }

    // POST: CLIENTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClienteExists(int? id)
    {
        return _context.Clientes.Any(e => e.Id == id);
    }
}
