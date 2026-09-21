using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;


public class ProdutosController : Controller
{
    private readonly CarCRMContext _context;

    public ProdutosController(CarCRMContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Cliente)
                .ThenInclude(c => c.Pessoa)
            .ToListAsync();

        var produtosViewModel = produtos.Select(p => new ProdutoViewModel
        {
            Id = p.Id,
            Nome = p.Nome,
            Valor = p.Valor,
            ClienteId = p.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = p.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = p.Cliente.Pessoa.Nome
                }
            }
        }).ToList();

        return View(produtosViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Cliente)
                .ThenInclude(c => c.Pessoa)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        var produtoViewModel = new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor,
            ClienteId = produto.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = produto.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = produto.Cliente.Pessoa.Nome
                }
            }
        };

        return View(produtoViewModel);
    }

    //GET/CREATE
    public IActionResult Create()
    {
        var produtoViewModel = new ProdutoViewModel();
        var clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();
        ViewBag.Clientes = clientes;
        return View(produtoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var produto = new Produto
            {
                Nome = produtoViewModel.Nome,
                Valor = produtoViewModel.Valor,
                ClienteId = produtoViewModel.ClienteId,
            };

            _context.Add(produto);
            await _context.SaveChangesAsync();
        }

        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();
        return RedirectToAction(nameof(Index));
    }

    //GET/EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Cliente)
                .ThenInclude(c => c.Pessoa)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();

        var produtoViewModel = new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor,
            ClienteId = produto.ClienteId,
        };

        return View(produtoViewModel);
    }

    //POST/EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ProdutoViewModel produtoViewModel)
    {
        if (id != produtoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var produto = await _context.Produtos
                    .Include(p => p.Cliente)
                        .ThenInclude (c => c.Pessoa)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if(produto == null)
                {
                    return NotFound();
                }

                produto.Nome = produtoViewModel.Nome;
                produto.Valor = produtoViewModel.Valor;
                produto.ClienteId = produtoViewModel.ClienteId;

                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ProdutoExists(produtoViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();
        return RedirectToAction(nameof(Index));

    }

    //GET/DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Cliente)
                .ThenInclude(c => c.Pessoa)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        var produtoViewModel = new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor,
            ClienteId = produto.ClienteId,
            Cliente = new ClienteViewModel
            {
                PessoaId = produto.Cliente.PessoaId,
                Pessoa = new PessoaViewModel
                {
                    Nome = produto.Cliente.Pessoa.Nome
                }
            }
        };

        return View(produtoViewModel);
    }

    //POST/DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto != null)
        {
            _context.Produtos.Remove(produto);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(int? id)
    {
        return _context.Produtos.Any(p => p.Id == id);
    }
}

