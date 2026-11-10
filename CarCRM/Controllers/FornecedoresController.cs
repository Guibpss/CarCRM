using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;

public class FornecedoresController : Controller
{
    private readonly CarCRMContext _context;

    public FornecedoresController(CarCRMContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        var fornecedores = await _context.Fornecedores
                .Include(f => f.Telefones)
                    .ThenInclude(t => t.TelefoneTipo)
            .Include(f => f.FornecedorTipo)
            .ToListAsync();

        var fornecedoresViewModel = fornecedores.Select(f => new FornecedorViewModel
        {
            Id = f.Id,
            Nome = f.Nome,
            Email = f.Email,
            Documento = f.Documento,
            FornecedorTipoId = f.FornecedorTipoId,
            FornecedorTipo = new FornecedorTipoViewModel
            {
                Id = f.FornecedorTipo.Id,
                Nome = f.FornecedorTipo.Nome,
            },
            Telefones = f.Telefones.Select(t => new TelefoneViewModel
            {
                Id = t.Id,
                DDD = t.DDD,
                Numero = t.Numero,
                TelefoneTipoId = t.TelefoneTipoId,
                PessoaId = t.PessoaId,
                TelefoneTipo = new TelefoneTipoViewModel
                {
                    Id = t.TelefoneTipo.Id,
                    Nome = t.TelefoneTipo.Nome,
                }
            }).ToList()

        }).ToList();

        return View(fornecedoresViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores
                .Include(f => f.Telefones)
                    .ThenInclude(t => t.TelefoneTipo)
            .Include(f => f.FornecedorTipo)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (fornecedor == null) 
        {
            return NotFound();
        }

        var fornecedorViewModel = new FornecedorViewModel
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Email = fornecedor.Email,
            Documento = fornecedor.Documento,
            FornecedorTipoId = fornecedor.FornecedorTipoId,
            FornecedorTipo = new FornecedorTipoViewModel
            {
                Id = fornecedor.FornecedorTipo.Id,
                Nome = fornecedor.FornecedorTipo.Nome,
            },
            Telefones = fornecedor.Telefones.Select(t => new TelefoneViewModel
            {
                Id = t.Id,
                DDD = t.DDD,
                Numero = t.Numero,
                TelefoneTipoId = t.TelefoneTipoId,
                PessoaId = t.PessoaId,
                TelefoneTipo = new TelefoneTipoViewModel
                {
                    Id = t.TelefoneTipo.Id,
                    Nome = t.TelefoneTipo.Nome
                }
            }).ToList()
        };

        return View(fornecedorViewModel);
    }

    //GET/CREATE
    public IActionResult Create()
    {
        var fornecedorViewModel = new FornecedorViewModel();
        var fornecedorTipos = _context.FornecedorTipos.OrderBy(x =>x.Nome).ToList();
        var telefoneTipos = _context.TelefonesTipo.OrderBy(x => x.Nome).ToList();
        ViewBag.FornecedorTipos = fornecedorTipos;
        ViewBag.TelefoneTipos = telefoneTipos;
        return View(fornecedorViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
    {
        ViewBag.FornecedorTipos = _context.FornecedorTipos.OrderBy(x => x.Nome).ToList();
        ViewBag.TelefoneTipos = _context.TelefonesTipo.OrderBy(x => x.Nome).ToList();

        if (ModelState.IsValid)
        {
            var fornecedor = new Fornecedor
            {
                Nome = fornecedorViewModel.Nome,
                Email = fornecedorViewModel.Email,
                Documento = fornecedorViewModel.Documento,
                FornecedorTipoId = fornecedorViewModel.FornecedorTipoId,
                Telefones = fornecedorViewModel.Telefones.Select(t => new Telefone
                {
                    DDD = t.DDD,
                    Numero = t.Numero,
                    TelefoneTipoId = t.TelefoneTipoId,
                }).ToList()
            };

            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    //GET/EDIT

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores
            .Include(f => f.Telefones)
                .ThenInclude(t => t.TelefoneTipo)
            .Include(f => f.FornecedorTipo)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fornecedor == null)
        {
            return NotFound();
        }

        ViewBag.FornecedorTipos = _context.FornecedorTipos.OrderBy(x => x.Nome).ToList();
        ViewBag.TelefoneTipos = _context.TelefonesTipo.OrderBy(x => x.Nome).ToList();

        var fornecedorViewModel = new FornecedorViewModel
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Email = fornecedor.Email,
            Documento = fornecedor.Documento,
            FornecedorTipoId = fornecedor.FornecedorTipoId,
            FornecedorTipo = new FornecedorTipoViewModel
            {
                Id = fornecedor.FornecedorTipo.Id,
                Nome = fornecedor.FornecedorTipo.Nome,
            },
            Telefones = fornecedor.Telefones.Select(t => new TelefoneViewModel
            {
                Id = t.Id,
                DDD = t.DDD,
                Numero = t.Numero,
                TelefoneTipoId = t.TelefoneTipoId,
                PessoaId = t.PessoaId,
                TelefoneTipo = new TelefoneTipoViewModel
                {
                    Id = t.TelefoneTipo.Id,
                    Nome = t.TelefoneTipo.Nome,
                }
            }).ToList()
        };

        return View(fornecedorViewModel);
    }

    //POST/EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, FornecedorViewModel fornecedorViewModel)
    {
        if (id != fornecedorViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var fornecedor = await _context.Fornecedores
                    .Include(f => f.Telefones)
                        .ThenInclude(t => t.TelefoneTipo)
                    .Include(f => f.FornecedorTipo)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (fornecedor == null)
                {
                    return NotFound();
                }

                fornecedor.Nome = fornecedorViewModel.Nome;
                fornecedor.Email = fornecedorViewModel.Email;
                fornecedor.Documento = fornecedorViewModel.Documento;
                fornecedor.FornecedorTipoId = fornecedorViewModel.FornecedorTipoId;
                fornecedor.Telefones = fornecedorViewModel.Telefones.Select(t => new Telefone
                {
                    Id = t.Id,
                    Numero = t.Numero,
                    DDD = t.DDD,
                    PessoaId = t.PessoaId,
                    TelefoneTipoId = t.TelefoneTipoId,
                    TelefoneTipo = new TelefoneTipo
                    {
                        Id = t.TelefoneTipo.Id,
                        Nome = t.TelefoneTipo.Nome,
                    }
                }).ToList();

                _context.Update(fornecedor);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {

                if (!FornecedorExists(fornecedorViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        ViewBag.FornecedorTipos = _context.FornecedorTipos.OrderBy(x => x.Nome).ToList();
        ViewBag.TelefoneTipos = _context.TelefonesTipo.OrderBy(x => x.Nome).ToList();
        return RedirectToAction(nameof(Index));
    }

    //GET/DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fornecedor = await _context.Fornecedores
            .Include(f => f.Telefones)
                .ThenInclude(t => t.TelefoneTipo)
            .Include(f => f.FornecedorTipo)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fornecedor == null) 
        {
            return NotFound();
        }

        var fornecedorViewModel = new FornecedorViewModel
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Email = fornecedor.Email,
            Documento = fornecedor.Documento,
            FornecedorTipoId = fornecedor.FornecedorTipoId,
            Telefones = fornecedor.Telefones.Select(f => new TelefoneViewModel
            {
                Id = f.Id,
                Numero = f.Numero,
                DDD = f.DDD,
                PessoaId = f.PessoaId,
                TelefoneTipoId = f.TelefoneTipoId,
                TelefoneTipo = new TelefoneTipoViewModel
                {
                    Id = f.TelefoneTipo.Id,
                    Nome = f.TelefoneTipo.Nome,
                }
            }).ToList()
        };

        return View(fornecedorViewModel);
    }

    //POST/DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);

        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FornecedorExists(int? id)
    {
        return _context.Fornecedores.Any(p => p.Id == id);
    }
}