
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCRM.Models;
using CarCRM.Data;

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
        var clientes = await _context.Clientes
            .Include(c => c.Pessoa)
            .ThenInclude(p => p.Telefones)
            .ThenInclude(t => t.TelefoneTipo)
            .ToListAsync();
        return View(clientes);
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
        ViewBag.telefonesTipo = _context.TelefonesTipo.ToList();
        return View(cliente);
    }

    // GET: CLIENTES/Create
    public IActionResult Create()
    {
        var cliente = new Cliente();
        var telefonesTipo = _context.TelefonesTipo.ToList();
        ViewBag.telefonesTipo = telefonesTipo;
        return View(cliente);
    }

    // POST: CLIENTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string tipoPessoa, string nome, string email, string cpf, string rg, 
    DateTime dataNascimento, string cnpj, string razaoSocial, string nomeFantasia, string nomeInterno, 
    string ddd, string numero, bool excluido, int telefonetipoId )
    {

        Pessoa pessoa = null;

        if (tipoPessoa == "pessoaFisica")
        {
            pessoa = new PessoaFisica
            {
                Nome = nome,
                Email = email,
                CPF = cpf,
                RG = rg,
                DataNascimento = dataNascimento

            };
        }
        else if (tipoPessoa == "pessoaJuridica")
        {
            pessoa = new PessoaJuridica
            {
                Nome = nome,
                Email= email,
                CNPJ = cnpj,
                RazaoSocial = razaoSocial,
                NomeFantasia = nomeFantasia,
                NomeInterno = nomeInterno,
            };
        }

        var cliente = new Cliente
        {
            CriadoEm = DateTime.Now,
            Excluido = excluido,
            Pessoa = pessoa
        };
    
            _context.Add(cliente);
            await _context.SaveChangesAsync();

        if (!string.IsNullOrWhiteSpace(numero))
        {
            var telefone = new Telefone
            {
                DDD = ddd,
                Numero = numero,
                TelefoneTipoId = telefonetipoId,
                PessoaId = pessoa.Id,
            };

            _context.Add(telefone);
            await _context.SaveChangesAsync();
        }
          
        return RedirectToAction(nameof(Index));

        ViewBag.telefonesTipo = _context.TelefonesTipo.ToList();
        return View(cliente);
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
        ViewBag.telefonesTipo = _context.TelefonesTipo.ToList();
        return View(cliente);
    }

    // POST: CLIENTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, string tipoPessoa, string nome, string email, string cpf, string rg,
    DateTime dataNascimento, string cnpj, string razaoSocial, string nomeFantasia, string nomeInterno,
    string ddd, string numero, bool excluido, int telefonetipoId)
    {
        var cliente = await _context.Clientes
           .Include(c => c.Pessoa)
           .ThenInclude(p => p.Telefones)
           .ThenInclude(t => t.TelefoneTipo)
           .FirstOrDefaultAsync(c => c.Id == id);

        if (id != cliente?.Id)
        {
            return NotFound();
        }

        cliente?.Pessoa.Nome = nome;
        cliente?.Pessoa.Email = email;
        cliente?.Pessoa.Excluido = excluido;

        if (cliente?.Pessoa is PessoaFisica pf)
        {
            pf.CPF = cpf;
            pf.RG = rg;
            pf.DataNascimento = dataNascimento;

        }

        else if (cliente.Pessoa is PessoaJuridica pj) 
        {
            pj.CNPJ = cnpj;
            pj.RazaoSocial = razaoSocial;
            pj.NomeFantasia = nomeFantasia;
            pj.NomeInterno = nomeInterno;
        
        }

        var telefone = cliente.Pessoa.Telefones.FirstOrDefault();

        if (telefone != null)
        {
            telefone.DDD = ddd;
            telefone.Numero = numero;
        }

        else if (!string.IsNullOrWhiteSpace(numero))
        {
             cliente.Pessoa.Telefones.Add(new Telefone
            {
                DDD = ddd,
                Numero = numero,
                TelefoneTipoId = telefonetipoId,
                PessoaId = cliente.PessoaId
            });

           
                _context.Update(cliente);
                await _context.SaveChangesAsync();
           
            
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
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

        ViewBag.telefonesTipo = _context.TelefonesTipo.ToList();
        return View(cliente);
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
