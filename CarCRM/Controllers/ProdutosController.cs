using Newtonsoft.Json;
using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            .Include(p => p.ProdutoTipo)
            .ToListAsync();

        var produtosViewModel = produtos.Select(p => new ProdutoViewModel
        {
            Id = p.Id,
            Nome = p.Nome,
            Valor = p.Valor,
            Quantidade = p.Quantidade,
            ProdutoTipoId = p.ProdutoTipoId,
            ProdutoTipo = new ProdutoTipoViewModel
            {
                Id = p.ProdutoTipo.Id,
                Nome = p.ProdutoTipo.Nome,
            }
        }).ToList();

        return View(produtosViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        var statusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
        var metodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();
        ViewBag.StatusPagamento = statusPagamento;
        ViewBag.MetodoPagamento = metodoPagamento;

        ViewBag.fornecedores = _context.Fornecedores.OrderBy(c => c.Nome).ToList();
        ViewBag.statuscompras = _context.StatusCompras.OrderBy(c => c.Nome).ToList();
        ViewBag.statusvendas = _context.StatusVendas.OrderBy(c => c.Nome).ToList();

        ViewBag.Clientes = _context.Clientes
            .Include(c => c.Pessoa)
            .OrderBy(c => c.Pessoa.Nome)
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Pessoa.Nome})
            .ToList();

        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.ProdutoTipo)
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
            Quantidade = produto.Quantidade,
            ProdutoTipoId = produto.ProdutoTipoId,
            ProdutoTipo = new ProdutoTipoViewModel
            {
                Id = produto.ProdutoTipo.Id,
                Nome = produto.ProdutoTipo.Nome,
            }
        };

        var compra = _context.CompraProdutos
            .Include(c => c.Fornecedor)
            .Include(c => c.StatusCompra)
            .FirstOrDefault(c => c.ProdutoId == id);

        var venda = _context.VendaProdutos
            .Include(v => v.Cliente)
                .ThenInclude(c => c.Pessoa)
            .Include(v => v.StatusVenda)
            .FirstOrDefault(v => v.ProdutoId == id);

        produtoViewModel.PagamentosProdutoCompra = new List<PagamentoViewModel>();
        produtoViewModel.PagamentosProdutoVenda = new List<PagamentoViewModel>();

        if (compra != null)
        {
            produtoViewModel.CompraProduto = new CompraProdutoViewModel
            {
                Id = compra.Id,
                CriadoEm = compra.CriadoEm,
                Excluido = compra.Excluido,
                DataCompra = compra.DataCompra,
                ValorCompra = compra.ValorCompra,
                Desconto = compra.Desconto,
                ProdutoId = compra.ProdutoId,
                FornecedorId = compra.FornecedorId,
                Fornecedor = new FornecedorViewModel
                {
                    Id = compra.Fornecedor.Id,
                    Nome = compra.Fornecedor.Nome,
                },
                StatusCompraId = compra.StatusCompraId,
                StatusCompra = new StatusCompraViewModel
                {
                    Id = compra.StatusCompra.Id,
                    Nome = compra.StatusCompra.Nome
                }
            };

            produtoViewModel.PagamentosProdutoCompra = _context.PagamentoCompraProdutos
                .Where(pc => pc.CompraProdutoId == compra.Id)
                .Select(pc => new PagamentoViewModel
                {
                    Id = pc.Pagamento.Id,
                    Valor = pc.Pagamento.Valor,
                    Parcelas = pc.Pagamento.Parcelas,
                    DataVencimento = pc.Pagamento.DataVencimento,
                    DataPagamento = pc.Pagamento.DataPagamento,
                    MetodoPagamentoId = pc.Pagamento.MetodoPagamentoId,
                    MetodoPagamento = new MetodoPagamentoViewModel
                    {
                        Id = pc.Pagamento.MetodoPagamento.Id,
                        Nome = pc.Pagamento.MetodoPagamento.Nome,
                    },
                    StatusPagamentoId = pc.Pagamento.StatusPagamentoId,
                    StatusPagamento = new StatusPagamentoViewModel
                    {
                        Id = pc.Pagamento.StatusPagamento.Id,
                        Nome = pc.Pagamento.StatusPagamento.Nome,
                    }
                }).ToList();
        }

        if (venda != null)
        {
            produtoViewModel.VendaProduto = new VendaProdutoViewModel
            {
                Id = venda.Id,
                CriadoEm = venda.CriadoEm,
                Excluido = venda.Excluido,
                DataVenda = venda.DataVenda,
                ValorVenda = venda.ValorVenda,
                Desconto = venda.Desconto,
                ProdutoId = venda.ProdutoId,
                ClienteId = venda.ClienteId,
                Cliente = new ClienteViewModel
                {
                    Id = venda.Cliente.Id,
                    PessoaId = venda.Cliente.PessoaId,
                    Pessoa = new PessoaViewModel
                    {
                        Id = venda.Cliente.Pessoa.Id,
                        Nome = venda.Cliente.Pessoa.Nome,
                    }
                },
                StatusVendaId = venda.StatusVendaId,
                StatusVenda = new StatusVendaViewModel
                {
                    Id = venda.StatusVenda.Id,
                    Nome = venda.StatusVenda.Nome,
                }
            };

            produtoViewModel.PagamentosProdutoVenda = _context.PagamentoVendaProdutos
                .Where(pv => pv.VendaProdutoId == venda.Id)
                .Select(pv => new PagamentoViewModel
                {
                    Id = pv.Pagamento.Id,
                    Valor = pv.Pagamento.Valor,
                    Parcelas = pv.Pagamento.Parcelas,
                    DataVencimento = pv.Pagamento.DataVencimento,
                    DataPagamento = pv.Pagamento.DataPagamento,
                    MetodoPagamentoId = pv.Pagamento.MetodoPagamentoId,
                    MetodoPagamento = new MetodoPagamentoViewModel
                    {
                        Id = pv.Pagamento.MetodoPagamento.Id,
                        Nome = pv.Pagamento.MetodoPagamento.Nome,
                    },
                    StatusPagamentoId = pv.Pagamento.StatusPagamentoId,
                    StatusPagamento = new StatusPagamentoViewModel
                    {
                        Id = pv.Pagamento.StatusPagamento.Id,
                        Nome = pv.Pagamento.StatusPagamento.Nome,
                    }
                }).ToList();
        }

        return View(produtoViewModel);

    }

    //GET/CREATE
    public IActionResult Create()
    {
        var produtoViewModel = new ProdutoViewModel();
        //var clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();
        var produtos = _context.ProdutoTipos.OrderBy(x => x.Nome).ToList();
        //ViewBag.Clientes = clientes;
        ViewBag.Produtos = produtos;
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
                Quantidade = produtoViewModel.Quantidade,
                ProdutoTipoId = produtoViewModel.ProdutoTipoId,
            };

            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Produtos = _context.ProdutoTipos.OrderBy(x => x.Nome).ToList();
        return View(produtoViewModel);
    }

    //GET/EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.ProdutoTipo)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        ViewBag.Produtos = _context.ProdutoTipos.OrderBy(x => x.Nome).ToList();

        var produtoViewModel = new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor,
            Quantidade = produto.Quantidade,
            ProdutoTipoId = produto.ProdutoTipoId,
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
                    .Include(p => p.ProdutoTipo)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if(produto == null)
                {
                    return NotFound();
                }

                produto.Nome = produtoViewModel.Nome;
                produto.Valor = produtoViewModel.Valor;
                produto.Quantidade = produtoViewModel.Quantidade;
                produto.ProdutoTipoId = produtoViewModel.ProdutoTipoId;

                _context.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        //ViewBag.Clientes = _context.Clientes.Include(c => c.Pessoa).OrderBy(x => x.Pessoa.Nome).ToList();
        ViewBag.Produtos = _context.ProdutoTipos.OrderBy(x => x.Nome).ToList();
        return View(produtoViewModel);

    }

    //GET/DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.ProdutoTipo)
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
            Quantidade = produto.Quantidade,
            ProdutoTipoId = produto.ProdutoTipoId,
            ProdutoTipo = new ProdutoTipoViewModel
            {
                Id = produto.ProdutoTipo.Id,
                Nome = produto.ProdutoTipo.Nome
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

    private decimal TotalPagoCompraProduto(int compraProdutoId, int? ignorarPagamentoId = null)
    {
        return _context.PagamentoCompraProdutos
            .Where(pc => pc.CompraProdutoId == compraProdutoId && pc.PagamentoId != ignorarPagamentoId)
            .Sum(pc => (decimal?)pc.Pagamento.Valor) ?? 0;
    }

    private decimal TotalPagoVendaProduto(int vendaProdutoId, int? ignorarPagamentoId = null)
    {
        return _context.PagamentoVendaProdutos
            .Where(pc => pc.VendaProdutoId == vendaProdutoId && pc.PagamentoId != ignorarPagamentoId)
            .Sum(pc => (decimal?)pc.Pagamento.Valor) ?? 0;
    }


    [HttpPost]
    public IActionResult AdicionarCompraProduto(CompraProdutoViewModel vm)
    {
        if (vm.ValorCompra == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Por favor, preencha o Valor" }), "application/json");
        }

        var compra = new CompraProduto
        {
            ProdutoId = vm.ProdutoId,
            DataCompra = vm.DataCompra,
            FornecedorId = vm.FornecedorId,
            StatusCompraId = vm.StatusCompraId,
            ValorCompra = vm.ValorCompra,
            Desconto = vm.Desconto,
            CriadoEm = DateTime.Now,
        };

        _context.CompraProdutos.Add(compra);
        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Compra cadastrada com sucesso!" }), "application/json");
    }

    public IActionResult ConsultarCompraProdutoPorId(int? id)
    {
        var compra = _context.CompraProdutos.Find(id);

        if (compra == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Não foi possível localizar a compra" }), "application/json");
        }

        var ptBR = new System.Globalization.CultureInfo("pt-BR");

        var resultado = new
        {
            Id = compra.Id,
            DataCompra = compra.DataCompra.ToString("yyyy-MM-dd"),
            FornecedorId = compra.FornecedorId,
            StatusCompraId = compra.StatusCompraId,
            ValorCompra = compra.ValorCompra.ToString("N2", ptBR),
            Desconto = compra.Desconto.ToString("N2", ptBR)
        };

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Resultado = resultado}), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> EditarCompraProduto(CompraProdutoViewModel vm)
    {
        if (vm.Id == 0 || vm.ValorCompra == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Compra inválida ou sem valor." }), "application/json");
        }

        var compra = await _context.CompraProdutos.FirstOrDefaultAsync(c => c.Id == vm.Id);
        if (compra == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Compra não encontrada." }), "application/json");
        }

        compra.DataCompra = vm.DataCompra;
        compra.FornecedorId = vm.FornecedorId;
        compra.StatusCompraId = vm.StatusCompraId;
        compra.ValorCompra = vm.ValorCompra;
        compra.Desconto = vm.Desconto;

        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Compra editada com sucesso!" }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirCompraProduto(int? id, int produtoId)
    {
        var compra = await _context.CompraProdutos.FindAsync(id);
        if (compra == null)
        {
            return NotFound();
        }

        var pagamentosCompraProduto = await _context.PagamentoCompraProdutos
            .Where(pc => pc.CompraProdutoId == id)
            .Select(pc => pc.PagamentoId)
            .ToListAsync();

        var vinculos = _context.PagamentoCompraProdutos
            .Where(pc => pc.CompraProdutoId == id);

        var pagamentos = _context.Pagamentos
            .Where(p => pagamentosCompraProduto.Contains(p.Id));


        _context.PagamentoCompraProdutos.RemoveRange(vinculos);
        _context.Pagamentos.RemoveRange(pagamentos);
        _context.CompraProdutos.Remove(compra);
        
        await _context.SaveChangesAsync();

        var retorno = new { Sucesso = true, Mensagem = "Compra excluída com sucesso!" };
        return RedirectToAction("Details", new
        {
            id = produtoId
        });
    }

    [HttpPost]
    public IActionResult AdicionarPagamentoProdutoCompra(PagamentoViewModel vm, int compraId)
    {
        if (vm.Valor == 0 || vm.MetodoPagamentoId == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Por favor, preencha valor e método de pagamento" }), "application/json");
        }

        var produtoCompra = _context.CompraProdutos.Find(compraId);

        if (produtoCompra == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Compra não encontrada." }), "application/json");
        }

        var totalCompra = Math.Round((decimal)produtoCompra.ValorFinal, 2);
        var pago = TotalPagoCompraProduto(compraId);

        if (pago + vm.Valor > totalCompra)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = $"O valor ultrapassa o total da compra. Restante: R$ {(totalCompra - pago):N2}" }), "application/json");
        }

        var pagamento = new Pagamento
        {
            Valor = vm.Valor,
            Parcelas = vm.MetodoPagamentoId == 2 ? vm.Parcelas : 1,
            DataVencimento = vm.DataVencimento,
            DataPagamento = vm.StatusPagamentoId == 1 ? vm.DataPagamento : default,
            MetodoPagamentoId = vm.MetodoPagamentoId,
            StatusPagamentoId = vm.StatusPagamentoId,
            CriadoEm = DateTime.Now
        };

        _context.Pagamentos.Add(pagamento);
        _context.SaveChanges();

        _context.PagamentoCompraProdutos.Add(new PagamentoCompraProduto
        {
            CompraProdutoId = compraId,
            PagamentoId = pagamento.Id
        });
        _context.SaveChanges();

        var retorno = new { Sucesso = true, Mensagem = "Pagamento da compra cadastrado com sucesso!" };
        return Content(JsonConvert.SerializeObject(retorno), "application/json");

    }

    public IActionResult ConsultarPagamentoCompraProdutoPorId(int? id)
    {
        var pagamento = _context.PagamentoCompraProdutos
            .Where(pc => pc.PagamentoId == id)
            .Select(pc => pc.Pagamento)
            .FirstOrDefault();

        if (pagamento == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Pagamento não encontrado." }), "application/json");
        }

        var ptBR = new System.Globalization.CultureInfo("pt-BR");

        var resultado = new
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor.ToString("N2", ptBR),
            Parcelas = pagamento.Parcelas,
            DataVencimento = pagamento.DataVencimento.ToString("yyyy-MM-dd"),
            DataPagamento = pagamento.DataPagamento == default ? "" : pagamento.DataPagamento.ToString("yyyy-MM-dd"),
            StatusPagamentoId = pagamento.StatusPagamentoId,
            MetodoPagamentoId = pagamento.MetodoPagamentoId
        };

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Resultado = resultado }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> EditarPagamentoCompraProduto(PagamentoViewModel vm)
    {
        if (vm.Id == 0 || vm.Valor == 0 || vm.MetodoPagamentoId == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Preencha valor e método de pagamento." }), "application/json");
        }

        var pagamento = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == vm.Id);

        if (pagamento == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Pagamento não encontrado." }), "application/json");
        }

        var compra = _context.PagamentoCompraProdutos
            .Where(pc => pc.PagamentoId == vm.Id)
            .Select(pc => pc.CompraProduto)
            .FirstOrDefault();

        if (compra == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Compra do pagamento não encontrada." }), "application/json");
        }

        var totalCompra = Math.Round((decimal)compra.ValorFinal, 2);
        var pago = TotalPagoCompraProduto(compra.Id, ignorarPagamentoId: vm.Id);

        if (pago + vm.Valor > totalCompra)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = $"O valor ultrapassa o total da compra. Restante: R$ {(totalCompra - pago):N2}" }), "application/json");
        }

        pagamento.Valor = vm.Valor;
        pagamento.Parcelas = vm.MetodoPagamentoId == 2 ? vm.Parcelas : 1;
        pagamento.DataVencimento = vm.DataVencimento;
        pagamento.DataPagamento = vm.StatusPagamentoId == 1 ? vm.DataPagamento : default;
        pagamento.MetodoPagamentoId = vm.MetodoPagamentoId;
        pagamento.StatusPagamentoId= vm.StatusPagamentoId;

        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Pagamento atualizado com sucesso!" }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirPagamentoProdutoCompra(int? id, int produtoId)
    {
        var vinculos = _context.PagamentoCompraProdutos.Where(pc => pc.PagamentoId == id);

        var pagamento = await _context.Pagamentos.FindAsync(id);

        if (pagamento == null)
        {
            return NotFound();
        }

        _context.PagamentoCompraProdutos.RemoveRange(vinculos);
        _context.Pagamentos.Remove(pagamento);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = produtoId });
    }


    //VENDA_PRODUTO

    [HttpPost]
    public IActionResult AdicionarVendaProduto(VendaProdutoViewModel vm)
    {
        if (vm.ValorVenda == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Por favor, preencha o Valor" }), "application/json");
        }

        var compraExists = _context.CompraProdutos
            .Any(c => c.ProdutoId == vm.ProdutoId);

        if (!compraExists)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Por favor, cadastre uma compra!" }), "application/json");
        }

        var venda = new VendaProduto
        {
            ProdutoId = vm.ProdutoId,
            DataVenda = vm.DataVenda,
            ClienteId = vm.ClienteId,
            StatusVendaId = vm.StatusVendaId,
            ValorVenda = vm.ValorVenda,
            Desconto = vm.Desconto,
            CriadoEm = DateTime.Now,
        };

        _context.VendaProdutos.Add(venda);
        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Venda cadastrada com sucesso!" }), "application/json");
    }

    public IActionResult ConsultarVendaProdutoPorId(int? id)
    {
        var venda = _context.VendaProdutos.Find(id);

        if (venda == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Não foi possível localizar a venda" }), "application/json");
        }

        var ptBR = new System.Globalization.CultureInfo("pt-BR");

        var resultado = new
        {
            Id = venda.Id,
            DataVenda = venda.DataVenda.ToString("yyyy-MM-dd"),
            ClienteId = venda.ClienteId,
            StatusVendaId = venda.StatusVendaId,
            ValorVenda = venda.ValorVenda.ToString("N2", ptBR),
            Desconto = venda.Desconto.ToString("N2", ptBR)
        };

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Resultado = resultado }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> EditarVendaProduto(VendaProdutoViewModel vm)
    {
        if (vm.Id == 0 || vm.ValorVenda == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Venda inválida ou sem valor." }), "application/json");
        }

        var venda = await _context.VendaProdutos.FirstOrDefaultAsync(c => c.Id == vm.Id);
        if (venda == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Venda não encontrada." }), "application/json");
        }

        venda.DataVenda = vm.DataVenda;
        venda.ClienteId = vm.ClienteId;
        venda.StatusVendaId = vm.StatusVendaId;
        venda.ValorVenda = vm.ValorVenda;
        venda.Desconto = vm.Desconto;

        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Venda editada com sucesso!" }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirVendaProduto(int? id, int produtoId)
    {
        var venda = await _context.VendaProdutos.FindAsync(id);
        if (venda == null)
        {
            return NotFound();
        }

        var pagamentosVendaProduto = await _context.PagamentoVendaProdutos
            .Where(pc => pc.VendaProdutoId == id)
            .Select(pc => pc.PagamentoId)
            .ToListAsync();

        var vinculos = _context.PagamentoVendaProdutos
            .Where(pc => pc.VendaProdutoId == id);

        var pagamentos = _context.Pagamentos
            .Where(p => pagamentosVendaProduto.Contains(p.Id));


        _context.PagamentoVendaProdutos.RemoveRange(vinculos);
        _context.Pagamentos.RemoveRange(pagamentos);
        _context.VendaProdutos.Remove(venda);

        await _context.SaveChangesAsync();

        var retorno = new { Sucesso = true, Mensagem = "Venda excluída com sucesso!" };
        return RedirectToAction("Details", new
        {
            id = produtoId
        });
    }

    [HttpPost]
    public IActionResult AdicionarPagamentoProdutoVenda(PagamentoViewModel vm, int vendaId)
    {
        if (vm.Valor == 0 || vm.MetodoPagamentoId == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Por favor, preencha valor e método de pagamento" }), "application/json");
        }

        var produtoVenda = _context.VendaProdutos.Find(vendaId);

        if (produtoVenda == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Venda não encontrada." }), "application/json");
        }

        var totalVenda = Math.Round((decimal)produtoVenda.ValorFinal, 2);
        var pago = TotalPagoVendaProduto(vendaId);

        if (pago + vm.Valor > totalVenda)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = $"O valor ultrapassa o total da venda. Restante: R$ {(totalVenda - pago):N2}" }), "application/json");
        }

        var pagamento = new Pagamento
        {
            Valor = vm.Valor,
            Parcelas = vm.MetodoPagamentoId == 2 ? vm.Parcelas : 1,
            DataVencimento = vm.DataVencimento,
            DataPagamento = vm.StatusPagamentoId == 1 ? vm.DataPagamento : default,
            MetodoPagamentoId = vm.MetodoPagamentoId,
            StatusPagamentoId = vm.StatusPagamentoId,
            CriadoEm = DateTime.Now
        };

        _context.Pagamentos.Add(pagamento);
        _context.SaveChanges();

        _context.PagamentoVendaProdutos.Add(new PagamentoVendaProduto
        {
            VendaProdutoId = vendaId,
            PagamentoId = pagamento.Id
        });
        _context.SaveChanges();

        var retorno = new { Sucesso = true, Mensagem = "Pagamento da venda cadastrado com sucesso!" };
        return Content(JsonConvert.SerializeObject(retorno), "application/json");

    }

    public IActionResult ConsultarPagamentoVendaProdutoPorId(int? id)
    {
        var pagamento = _context.PagamentoVendaProdutos
            .Where(pc => pc.PagamentoId == id)
            .Select(pc => pc.Pagamento)
            .FirstOrDefault();

        if (pagamento == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Pagamento não encontrado." }), "application/json");
        }

        var ptBR = new System.Globalization.CultureInfo("pt-BR");

        var resultado = new
        {
            Id = pagamento.Id,
            Valor = pagamento.Valor.ToString("N2", ptBR),
            Parcelas = pagamento.Parcelas,
            DataVencimento = pagamento.DataVencimento.ToString("yyyy-MM-dd"),
            DataPagamento = pagamento.DataPagamento == default ? "" : pagamento.DataPagamento.ToString("yyyy-MM-dd"),
            StatusPagamentoId = pagamento.StatusPagamentoId,
            MetodoPagamentoId = pagamento.MetodoPagamentoId
        };

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Resultado = resultado }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> EditarPagamentoVendaProduto(PagamentoViewModel vm)
    {
        if (vm.Id == 0 || vm.Valor == 0 || vm.MetodoPagamentoId == 0)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Preencha valor e método de pagamento." }), "application/json");
        }

        var pagamento = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == vm.Id);

        if (pagamento == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Pagamento não encontrado." }), "application/json");
        }

        var venda = _context.PagamentoVendaProdutos
            .Where(pc => pc.PagamentoId == vm.Id)
            .Select(pc => pc.VendaProduto)
            .FirstOrDefault();

        if (venda == null)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = "Venda do pagamento não encontrada." }), "application/json");
        }

        var totalVenda = Math.Round((decimal)venda.ValorFinal, 2);
        var pago = TotalPagoCompraProduto(venda.Id, ignorarPagamentoId: vm.Id);

        if (pago + vm.Valor > totalVenda)
        {
            return Content(JsonConvert.SerializeObject(new { Sucesso = false, Mensagem = $"O valor ultrapassa o total da venda. Restante: R$ {(totalVenda - pago):N2}" }), "application/json");
        }

        pagamento.Valor = vm.Valor;
        pagamento.Parcelas = vm.MetodoPagamentoId == 2 ? vm.Parcelas : 1;
        pagamento.DataVencimento = vm.DataVencimento;
        pagamento.DataPagamento = vm.StatusPagamentoId == 1 ? vm.DataPagamento : default;
        pagamento.MetodoPagamentoId = vm.MetodoPagamentoId;
        pagamento.StatusPagamentoId = vm.StatusPagamentoId;

        _context.SaveChanges();

        return Content(JsonConvert.SerializeObject(new { Sucesso = true, Mensagem = "Pagamento atualizado com sucesso!" }), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirPagamentoVendaProduto(int? id, int produtoId)
    {
        var vinculos = _context.PagamentoVendaProdutos.Where(pc => pc.PagamentoId == id);

        var pagamento = await _context.Pagamentos.FindAsync(id);

        if (pagamento == null)
        {
            return NotFound();
        }

        _context.PagamentoVendaProdutos.RemoveRange(vinculos);
        _context.Pagamentos.Remove(pagamento);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = produtoId });
    }
}

