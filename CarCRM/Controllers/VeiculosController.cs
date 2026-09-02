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
using NuGet.ProjectModel;
using Newtonsoft.Json;

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
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoVersao)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .Include(v => v.VeiculoTransmissao);
            var veiculos = await carCRMContext.ToListAsync();

            var veiculosViewModel = veiculos.Select(v =>
            new VeiculoViewModel
            {
                Id = v.Id,
                AnoFabricacao = v.AnoFabricacao,
                AnoModelo = v.AnoModelo,
                VeiculoCor = new VeiculoCorViewModel
                {
                    Id = v.VeiculoCor.Id,
                    Nome = v.VeiculoCor.Nome
                },
                VeiculoMarca = new VeiculoMarcaViewModel
                {
                    Id = v.VeiculoMarca.Id,
                    Nome = v.VeiculoMarca.Nome
                },
                VeiculoModelo = new VeiculoModeloViewModel
                {
                    Id = v.VeiculoModelo.Id,
                    Nome = v.VeiculoModelo.Nome
                },
                KilometragemAtual = v.KilometragemAtual,
                Placa = v.Placa,
                Renavam = v.Renavam,
                VeiculoTransmissaoId = v.VeiculoTransmissaoId,
                VeiculoTransmissao = v.VeiculoTransmissao == null ? null : new VeiculoTransmissaoViewModel
                {
                    Id = v.VeiculoTransmissao.Id,
                    Nome = v.VeiculoTransmissao.Nome
                },
                VeiculoCombustivel = new VeiculoCombustivelViewModel
                {
                    Id = v.VeiculoCombustivel.Id,
                    Nome = v.VeiculoCombustivel.Nome
                },
                VeiculoMotorizacao = new VeiculoMotorizacaoViewModel
                {
                    Id = v.VeiculoMotorizacao.Id,
                    Nome = v.VeiculoMotorizacao.Nome
                },
                VeiculoTipo = new VeiculoTipoViewModel
                {
                    Id = v.VeiculoTipo.Id,
                    Nome = v.VeiculoTipo.Nome
                },
                VeiculoVersao = new VeiculoVersaoViewModel
                {
                    Id = v.VeiculoVersao.Id,
                    Nome = v.VeiculoVersao.Nome
                }

            }).ToList();

            return View(veiculosViewModel);
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var statusPagamento = _context.StatusPagamentos.OrderBy(p => p.Nome).ToList();
            var metodoPagamento = _context.MetodosPagamento.OrderBy(p => p.Nome).ToList();
            ViewBag.StatusPagamento = statusPagamento;
            ViewBag.MetodoPagamento = metodoPagamento;

            ViewBag.vendedores = _context.Usuarios.OrderBy(c => c.Nome).ToList();
            ViewBag.veiculos = _context.Veiculos.ToList();
            ViewBag.statuscompras = _context.StatusCompras.OrderBy(c => c.Nome).ToList();

            ViewBag.clientes = _context.Clientes
                .Include(c => c.Pessoa)
                .OrderBy(c => c.Pessoa.Nome)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Pessoa.Nome })
                .ToList();
            ViewBag.statusvendas = _context.StatusVendas.OrderBy(s => s.Nome).ToList();

            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoVersao)
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .Include(v => v.VeiculoTransmissao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var compra = _context.Compras
                .Include(c => c.Vendedor)
                .Include(c => c.StatusCompra)
                .Include(c => c.Pagamentos)
                .Where(c => c.VeiculoId == id)
                .FirstOrDefault();

            var venda = _context.Vendas
                .Include(v => v.Cliente)
                    .ThenInclude(c => c.Pessoa)
                .Include(v => v.StatusVenda)
                .Include(v => v.Pagamentos)
                .Where(v => v.VeiculoId == id)
                .FirstOrDefault();
            
            var veiculoViewModel = new VeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Renavam = veiculo.Renavam,
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
                VeiculoTransmissaoId = veiculo.VeiculoTransmissaoId,
                VeiculoTransmissao = veiculo.VeiculoTransmissao == null ? null : new VeiculoTransmissaoViewModel
                {
                    Id = veiculo.VeiculoTransmissao.Id,
                    Nome = veiculo.VeiculoTransmissao.Nome
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
                VeiculoVersao = new VeiculoVersaoViewModel
                {
                    Id = veiculo.VeiculoVersao.Id,
                    Nome = veiculo.VeiculoVersao.Nome
                },
                CriadoEm = veiculo.CriadoEm,
                Excluido = veiculo.Excluido,                                
            };

            if(compra != null)
            {
                veiculoViewModel.Compra = new CompraViewModel
                {
                    Id = compra.Id,
                    CriadoEm = compra.CriadoEm,
                    Excluido = compra.Excluido,
                    DataCompra = compra.DataCompra,
                    ValorCompra = compra.ValorCompra,
                    Desconto = compra.Desconto,
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
                        Nome = compra.StatusCompra.Nome,
                    }
                };

                veiculoViewModel.PagamentosCompra = compra.Pagamentos == null ? null : compra.Pagamentos.Select(p => new PagamentoViewModel
                {
                    Id = p.Id,
                    Valor = p.Valor,
                    Parcelas = p.Parcelas,
                    DataVencimento = p.DataVencimento,
                    DataPagamento = p.DataPagamento,
                    MetodoPagamentoId = p.MetodoPagamentoId,
                    MetodoPagamento = new MetodoPagamentoViewModel
                    {
                        Id = p.MetodoPagamento.Id,
                        Nome = p.MetodoPagamento.Nome,
                    },
                    StatusPagamentoId = p.StatusPagamentoId,
                    StatusPagamento = new StatusPagamentoViewModel
                    {
                        Id = p.StatusPagamento.Id,
                        Nome = p.StatusPagamento.Nome,
                    }
                }).ToList();
            }

            if(venda != null)
            {
                veiculoViewModel.Venda = new VendaViewModel
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
                        Pessoa = new PessoaViewModel
                        {
                            Id = venda.Cliente.Pessoa.Id,
                            Nome = venda.Cliente.Pessoa.Nome,
                            Email = venda.Cliente.Pessoa.Email,
                        }
                    },
                    VeiculoId = venda.VeiculoId,
                    Veiculo = new VeiculoViewModel
                    {
                        Id = veiculo.Id,
                        Placa = veiculo.Placa,
                    },
                    StatusVendaId = venda.StatusVendaId,
                    StatusVenda = new StatusVendaViewModel
                    {
                        Id = venda.StatusVenda.Id,
                        Nome = venda.StatusVenda.Nome,
                    }
                };

                veiculoViewModel.PagamentosVenda = venda.Pagamentos == null ? null : venda.Pagamentos.Select(p => new PagamentoViewModel
                {
                    Id = p.Id,
                    Valor = p.Valor,
                    Parcelas = p.Parcelas,
                    DataVencimento = p.DataVencimento,
                    DataPagamento = p.DataPagamento,
                    MetodoPagamentoId = p.MetodoPagamentoId,
                    MetodoPagamento = new MetodoPagamentoViewModel
                    {
                        Id = p.MetodoPagamento.Id,
                        Nome = p.MetodoPagamento.Nome,
                    },
                    StatusPagamentoId = p.StatusPagamentoId,
                    StatusPagamento = new StatusPagamentoViewModel
                    {
                        Id = p.StatusPagamento.Id,
                        Nome = p.StatusPagamento.Nome,
                    }
                }).ToList();
            }


            return View(veiculoViewModel);

        }

        [HttpGet]
        public JsonResult ModelosPorMarca(int marcaId)
        {
            var modeloViewModel = _context.VeiculoModelo
                .Where(m => m.VeiculoMarcaId == marcaId)
                .Select(m => new { m.Id, m.Nome })
                .ToList();

            return Json(modeloViewModel);
        }

        [HttpGet]
        public JsonResult VersaoPorModelos(int modeloId)
        {
            var versaoViewModel = _context.VeiculoVersao
                .Where(m => m.VeiculoModeloId == modeloId)
                .Select(m => new { m.Id, m.Nome })
                .ToList();

            return Json(versaoViewModel);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            var veiculo = new Veiculo();
            ViewBag.veiculosCor = _context.VeiculoCor.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosVersao = _context.VeiculoVersao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTransmissao = _context.VeiculoTransmissao.OrderBy(x => x.Nome).ToList();

            return View(new VeiculoViewModel());
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoViewModel veiculoViewModel)
        {
            if (ModelState.IsValid)
            {
                var veiculo = new Veiculo
                {
                    VeiculoCorId = veiculoViewModel.VeiculoCorId,
                    KilometragemAtual = veiculoViewModel.KilometragemAtual,
                    Placa = veiculoViewModel.Placa,
                    Renavam = veiculoViewModel.Renavam,
                    VeiculoCombustivelId = veiculoViewModel.VeiculoCombustivelId,
                    VeiculoMotorizacaoId = veiculoViewModel.VeiculoMotorizacaoId,
                    VeiculoTransmissaoId = veiculoViewModel.VeiculoTransmissaoId,
                    AnoFabricacao = veiculoViewModel.AnoFabricacao,
                    AnoModelo = veiculoViewModel.AnoModelo,
                    VeiculoTipoId = veiculoViewModel.VeiculoTipoId,
                    VeiculoMarcaId = veiculoViewModel.VeiculoMarcaId,
                    VeiculoModeloId = veiculoViewModel.VeiculoModeloId,
                    VeiculoVersaoId = veiculoViewModel.VeiculoVersaoId,
                    Excluido = veiculoViewModel.Excluido
                };

                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.veiculosCor = _context.VeiculoCor.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosVersao = _context.VeiculoVersao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTransmissao = _context.VeiculoTransmissao.OrderBy(x => x.Nome).ToList();
            return View(veiculoViewModel);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.VeiculoMarca)
                .Include(v => v.VeiculoModelo)
                .Include(v => v.VeiculoVersao)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .Include(v => v.VeiculoTransmissao)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            if (veiculo == null)
            {
                return NotFound();
            }

            ViewBag.veiculosCor = _context.VeiculoCor.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosVersao = _context.VeiculoVersao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTransmissao = _context.VeiculoTransmissao.OrderBy(x => x.Nome).ToList();

            var veiculoViewModel = new VeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Renavam = veiculo.Renavam,
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
                VeiculoTransmissao = veiculo.VeiculoTransmissao == null ? null : new VeiculoTransmissaoViewModel
                {
                    Id = veiculo.VeiculoTransmissao.Id,
                    Nome = veiculo.VeiculoTransmissao.Nome
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
                VeiculoVersao = new VeiculoVersaoViewModel
                {
                    Id = veiculo.VeiculoVersao.Id,
                    Nome = veiculo.VeiculoVersao.Nome
                },
                VeiculoCorId = veiculo.VeiculoCorId,
                VeiculoCombustivelId = veiculo.VeiculoCombustivelId,
                VeiculoMotorizacaoId = veiculo.VeiculoMotorizacaoId,
                VeiculoTransmissaoId = veiculo.VeiculoTransmissaoId,
                VeiculoTipoId = veiculo.VeiculoTipoId,
                VeiculoMarcaId = veiculo.VeiculoMarcaId,
                VeiculoModeloId = veiculo.VeiculoModeloId,
                VeiculoVersaoId = veiculo.VeiculoVersaoId,
                Excluido = veiculo.Excluido
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
                    veiculo.Renavam = veiculoViewModel.Renavam;
                    veiculo.VeiculoCombustivelId = veiculoViewModel.VeiculoCombustivelId;
                    veiculo.VeiculoMotorizacaoId = veiculoViewModel.VeiculoMotorizacaoId;
                    veiculo.VeiculoTransmissaoId = veiculoViewModel.VeiculoTransmissaoId;
                    veiculo.AnoFabricacao = veiculoViewModel.AnoFabricacao;
                    veiculo.AnoModelo = veiculoViewModel.AnoModelo;
                    veiculo.VeiculoTipoId = veiculoViewModel.VeiculoTipoId;
                    veiculo.VeiculoMarcaId = veiculoViewModel.VeiculoMarcaId;
                    veiculo.VeiculoModeloId = veiculoViewModel.VeiculoModeloId;
                    veiculo.VeiculoVersaoId = veiculoViewModel.VeiculoVersaoId;
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
            ViewBag.veiculosCor = _context.VeiculoCor.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMarca = _context.veiculoMarcas.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosModelo = _context.VeiculoModelo.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosVersao = _context.VeiculoVersao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTipo = _context.VeiculoTipos.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosCombustivel = _context.VeiculoCombustivel.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosMotorizacao = _context.VeiculoMotorizacao.OrderBy(x => x.Nome).ToList();
            ViewBag.veiculosTransmissao = _context.VeiculoTransmissao.OrderBy(x => x.Nome).ToList();
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
                .Include(v => v.VeiculoVersao)
                .Include(v => v.VeiculoTipo)
                .Include(v => v.VeiculoCor)
                .Include(v => v.VeiculoCombustivel)
                .Include(v => v.VeiculoMotorizacao)
                .Include(v => v.VeiculoTransmissao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var veiculoViewModel = new VeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Renavam = veiculo.Renavam,
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
                VeiculoTransmissaoId = veiculo.VeiculoTransmissaoId,
                VeiculoTransmissao = veiculo.VeiculoTransmissao == null ? null : new VeiculoTransmissaoViewModel
                {
                    Id = veiculo.VeiculoTransmissao.Id,
                    Nome = veiculo.VeiculoTransmissao.Nome
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
                VeiculoVersao = new VeiculoVersaoViewModel
                {
                    Id = veiculo.VeiculoVersao.Id,
                    Nome = veiculo.VeiculoVersao.Nome
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

        [HttpPost]
        public IActionResult AdicionarPagamento(PagamentoViewModel pagamentoViewModel)
        {
            if (pagamentoViewModel.Valor == 0 || pagamentoViewModel.MetodoPagamentoId == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor e método de pagamento" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var pagamento = new Pagamento();
            //pagamento.VeiculoId = pagamentoViewModel.VeiculoId;
            pagamento.Valor = pagamentoViewModel.Valor;
            pagamento.DataVencimento = pagamentoViewModel.DataVencimento;
            pagamento.DataPagamento = pagamentoViewModel.DataPagamento;
            pagamento.Parcelas = pagamentoViewModel.Parcelas;
            pagamento.MetodoPagamentoId = pagamentoViewModel.MetodoPagamentoId;
            pagamento.StatusPagamentoId = pagamentoViewModel.StatusPagamentoId;
            pagamento.CriadoEm = DateTime.Now;

            _context.Pagamentos.Add(pagamento);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Pagamento cadastrado com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        public IActionResult AdicionarPagamentoCompra(PagamentoViewModel pagamentoViewModel)
        {
            if (pagamentoViewModel.Valor == 0 || pagamentoViewModel.MetodoPagamentoId == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor e método de pagamento" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var pagamento = new Pagamento();
            pagamento.Valor = pagamentoViewModel.Valor;
            pagamento.DataVencimento = pagamentoViewModel.DataVencimento;
            pagamento.DataPagamento = pagamentoViewModel.DataPagamento;
            pagamento.Parcelas = pagamentoViewModel.Parcelas;
            pagamento.MetodoPagamentoId = pagamentoViewModel.MetodoPagamentoId;
            pagamento.StatusPagamentoId = pagamentoViewModel.StatusPagamentoId;
            pagamento.CriadoEm = DateTime.Now;

            _context.Pagamentos.Add(pagamento);
            _context.SaveChanges();

            var compra = _context.Compras.Where(c => c.VeiculoId == pagamentoViewModel.VeiculoId).FirstOrDefault();
            var pagamentoCompra = new PagamentoCompra();
            pagamentoCompra.CompraId = compra.Id;
            pagamentoCompra.PagamentoId = pagamento.Id;
            _context.PagamentoCompras.Add(pagamentoCompra);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Pagamento cadastrado com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }


        [HttpPost]
        public IActionResult AdicionarCompra(CompraViewModel compraViewModel)
        {
            if (compraViewModel.ValorCompra == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var compra = new Compra();
            compra.VeiculoId = compraViewModel.VeiculoId;
            compra.DataCompra = compraViewModel.DataCompra;
            compra.VendedorId = compraViewModel.VendedorId;
            compra.StatusCompraId = compraViewModel.StatusCompraId;
            compra.ValorCompra = compraViewModel.ValorCompra;
            compra.Desconto = compraViewModel.Desconto;
            compra.CriadoEm = DateTime.Now;

            _context.Compras.Add(compra);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Compra cadastrada com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        public IActionResult ConsultarPagamentoPorId(int id)
        {
            var pagamento = _context.Pagamentos.Find(id);
            if (pagamento == null)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Não foi possível localizar o pagamento" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var resultado = new
            {
                Id = pagamento.Id,
                Valor = pagamento.Valor,
                Parcelas = pagamento.Parcelas,
                DataVencimento = pagamento.DataVencimento.ToString("yyyy-MM-dd"),
                DataPagamento = pagamento.DataPagamento == default
            ? "" : pagamento.DataPagamento.ToString("yyyy-MM-dd"),
                StatusPagamentoId = pagamento.StatusPagamentoId,
                MetodoPagamentoId = pagamento.MetodoPagamentoId
            };

            var retorno = new { Sucesso = true, Resultado = resultado };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        public IActionResult ConsultarCompraPorId(int id)
        {
            var compra = _context.Compras.Find(id);
            if (compra == null)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Não foi possível localizar a compra" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var resultado = new
            {
                Id = compra.Id,
                DataCompra = compra.DataCompra.ToString("yyyy-MM-dd"),
                VeiculoId = compra.VeiculoId,
                VendedorId = compra.VendedorId,
                StatusCompraId = compra.StatusCompraId,
                ValorCompra = compra.ValorCompra
            };

            var retorno = new { Sucesso = true, Resultado = resultado };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> EditarPagamento(PagamentoViewModel pagamentoViewModel)
        {
            if (pagamentoViewModel.Id == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Pagamento Inválido." };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            if (pagamentoViewModel.Valor == 0 || pagamentoViewModel.MetodoPagamentoId == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor e método de pagamento" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }


            var pagamento = await _context.Pagamentos.FirstOrDefaultAsync(p => p.Id == pagamentoViewModel.Id);

            if (pagamento == null)
            {
                var erroNaoEncontrado = new { Sucesso = false, Mensagem = "Pagamento não encontrado." };
                return Content(JsonConvert.SerializeObject(erroNaoEncontrado), "application/json");
            }

            //pagamento.VeiculoId = pagamentoViewModel.VeiculoId;
            pagamento.Valor = pagamentoViewModel.Valor;
            pagamento.DataVencimento = pagamentoViewModel.DataVencimento;
            pagamento.DataPagamento = pagamentoViewModel.DataPagamento;
            pagamento.Parcelas = pagamentoViewModel.Parcelas;
            pagamento.MetodoPagamentoId = pagamentoViewModel.MetodoPagamentoId;
            pagamento.StatusPagamentoId = pagamentoViewModel.StatusPagamentoId;

            _context.Update(pagamento);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Pagamento cadastrado com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> EditarCompra(CompraViewModel compraViewModel)
        {
            if (compraViewModel.Id == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Compra Inválida." };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            if (compraViewModel.ValorCompra == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor da compra" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }


            var compra = await _context.Compras.FirstOrDefaultAsync(p => p.Id == compraViewModel.Id);

            if (compra == null)
            {
                var erroNaoEncontrado = new { Sucesso = false, Mensagem = "Compra não encontrada." };
                return Content(JsonConvert.SerializeObject(erroNaoEncontrado), "application/json");
            }

            compra.DataCompra = compraViewModel.DataCompra;
            compra.VendedorId = compraViewModel.VendedorId;
            compra.StatusCompraId = compraViewModel.StatusCompraId;
            compra.ValorCompra = compraViewModel.ValorCompra;
            compra.Desconto = compraViewModel.Desconto;

            _context.Update(compra);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Compra editada com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirPagamento(int? id, int veiculoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            _context.SaveChangesAsync();

            var retorno = new { Sucesso = true, Mensagem = "Pagamento excluído com sucesso!" };
            return RedirectToAction("Details", new { id = veiculoId });
        }

        public async Task<IActionResult> ExcluirCompra(int? id, int veiculoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(compra);
            _context.SaveChangesAsync();

            var retorno = new { Sucesso = true, Mensagem = "Compra excluída com sucesso!" };
            return RedirectToAction("Details", new { id = veiculoId });
        }

        [HttpPost]
        public IActionResult AdicionarVenda(VendaViewModel vendaViewModel)
        {
            if (vendaViewModel.ValorVenda == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var venda = new Venda();
            venda.VeiculoId = vendaViewModel.VeiculoId;
            venda.DataVenda = vendaViewModel.DataVenda;
            venda.ClienteId = vendaViewModel.ClienteId;
            venda.StatusVendaId = vendaViewModel.StatusVendaId;
            venda.ValorVenda = vendaViewModel.ValorVenda;
            venda.Desconto = vendaViewModel.Desconto;
            venda.CriadoEm = DateTime.Now;

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Venda cadastrada com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        public IActionResult ConsultarVendaPorId(int id)
        {
            var venda = _context.Vendas.Find(id);
            if (venda == null)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Não foi possível localizar a venda" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var resultado = new
            {
                Id = venda.Id,
                DataVenda = venda.DataVenda.ToString("yyyy-MM-dd"),
                VeiculoId = venda.VeiculoId,
                ClienteId = venda.ClienteId,
                StatusVendaId = venda.StatusVendaId,
                ValorVenda = venda.ValorVenda.ToString("N2"),
                Desconto = venda.Desconto.ToString("N2")
            };

            var retorno = new { Sucesso = true, Resultado = resultado };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> EditarVenda(VendaViewModel vendaViewModel)
        {
            if (vendaViewModel.Id == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Venda Inválida." };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            if (vendaViewModel.ValorVenda == 0)
            {
                var retornoErro = new { Sucesso = false, Mensagem = "Por favor, preencha valor da venda" };
                return Content(JsonConvert.SerializeObject(retornoErro), "application/json");
            }

            var venda = await _context.Vendas.FirstOrDefaultAsync(v => v.Id == vendaViewModel.Id);

            if (venda == null)
            {
                var erroNaoEncontrado = new { Sucesso = false, Mensagem = "Venda não encontrada." };
                return Content(JsonConvert.SerializeObject(erroNaoEncontrado), "application/json");
            }

            venda.VeiculoId = vendaViewModel.VeiculoId;
            venda.DataVenda = vendaViewModel.DataVenda;
            venda.ClienteId = vendaViewModel.ClienteId;
            venda.StatusVendaId = vendaViewModel.StatusVendaId;
            venda.ValorVenda = vendaViewModel.ValorVenda;
            venda.Desconto = vendaViewModel.Desconto;

            _context.Update(venda);
            _context.SaveChanges();

            var retorno = new { Sucesso = true, Mensagem = "Venda editada com sucesso!" };
            return Content(JsonConvert.SerializeObject(retorno), "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirVenda(int? id, int veiculoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = veiculoId });
        }
    }
}
