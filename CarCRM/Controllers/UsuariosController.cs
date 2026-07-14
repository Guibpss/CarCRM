
using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class UsuariosController : Controller
{
    private readonly CarCRMContext _context;

    public UsuariosController(CarCRMContext context)
    {
        _context = context;
    }

    // GET: USUARIOS
    public async Task<IActionResult> Index()    
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Pessoa)
            .Include(u => u.Perfil)
            .ToListAsync();

        var usuariosViewModel = usuarios.Select(u => new UsuarioViewModel
        {
            Id = u.Id,
            Nome = u.Nome,
            CriadoEm = u.CriadoEm,
            Ativo = u.Ativo,
            PerfilId = u.PerfilId,
            Perfil = u.Perfil == null ? null : new PerfilViewModel
            {
                Id = u.Perfil.Id,
                Nome = u.Perfil.Nome
            },
            PessoaId = u.PessoaId,
            Pessoa = new PessoaFisicaViewModel
            {
                Id = u.Pessoa.Id,
                Nome = u.Pessoa.Nome,
                Email = u.Pessoa.Email,
                CPF = u.Pessoa.CPF,
                RG = u.Pessoa.RG,
                DataNascimento = u.Pessoa.DataNascimento
            }
        }).ToList();

        return View(usuariosViewModel);
    }

    // GET: USUARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
       .Include(u => u.Pessoa)
       .Include(u => u.Perfil)
       .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            CriadoEm = usuario.CriadoEm,
            Ativo = usuario.Ativo,
            PerfilId = usuario.PerfilId,
            Perfil = usuario.Perfil == null ? null : new PerfilViewModel
            {
                Id = usuario.Perfil.Id,
                Nome = usuario.Perfil.Nome
            },
            PessoaId = usuario.PessoaId,
            Pessoa = new PessoaFisicaViewModel
            {
                Id = usuario.Pessoa.Id,
                Nome = usuario.Pessoa.Nome,
                Email = usuario.Pessoa.Email,
                CPF = usuario.Pessoa.CPF,
                RG = usuario.Pessoa.RG,
                DataNascimento = usuario.Pessoa.DataNascimento
            }
        };

        return View(usuarioViewModel);
    }

    // GET: USUARIOS/Create
    public IActionResult Create()
    {
        var usuario = new Usuario();
        var perfis = _context.Perfis.ToList();
        ViewBag.Perfis = perfis;
        return View(usuario);
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsuarioViewModel usuarioViewModel)
    {

        if (ModelState.IsValid)
        {
            var usuario = new Usuario
            {
                Nome = usuarioViewModel.Nome,
                Senha = usuarioViewModel.Senha,
                ConfirmaSenha = usuarioViewModel.ConfirmaSenha,
                CriadoEm = usuarioViewModel.CriadoEm,
                Ativo = usuarioViewModel.Ativo,
                PerfilId = usuarioViewModel.PerfilId,

                Pessoa = new PessoaFisica
                {
                    Nome = usuarioViewModel.Nome,
                    Email = usuarioViewModel.Pessoa.Email,
                    CPF = usuarioViewModel.Pessoa.CPF,
                    RG = usuarioViewModel.Pessoa.RG,
                    DataNascimento = usuarioViewModel.Pessoa.DataNascimento,
                }
            };
            
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Perfis = _context.Perfis.ToList();
        return View(usuarioViewModel);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
        .Include(u => u.Pessoa)
        .Include(u => u.Perfil)
        .FirstOrDefaultAsync(u => u.Id == id);
  
        if (usuario == null)
        {
            return NotFound();
        }

        ViewBag.Perfis = _context.Perfis.ToList();

        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            CriadoEm = usuario.CriadoEm,
            Ativo = usuario.Ativo,
            PerfilId = usuario.PerfilId,
            Perfil = usuario.Perfil == null ? null : new PerfilViewModel
            {
                Id = usuario.Perfil.Id,
                Nome = usuario.Perfil.Nome
            },
            PessoaId = usuario.PessoaId,
            Pessoa = new PessoaFisicaViewModel
            {
                Id = usuario.Pessoa.Id,
                Nome = usuario.Pessoa.Nome,
                Email = usuario.Pessoa.Email,
                CPF = usuario.Pessoa.CPF,
                RG = usuario.Pessoa.RG,
                DataNascimento = usuario.Pessoa.DataNascimento
            }
        };
        return View(usuarioViewModel);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, UsuarioViewModel usuarioViewModel)
    {
        if (id != usuarioViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Pessoa)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                usuario.Nome = usuarioViewModel.Nome;
                usuario.Ativo = usuarioViewModel.Ativo;
                usuario.PerfilId = usuarioViewModel.PerfilId;
                usuario.Pessoa.Nome = usuarioViewModel.Nome;
                usuario.Pessoa.DataNascimento = usuarioViewModel.Pessoa.DataNascimento;

                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuarioViewModel.Id))
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
        return View(usuarioViewModel);
    }

    // GET: USUARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
       .Include(u => u.Pessoa)
       .Include(u => u.Perfil)
       .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            CriadoEm = usuario.CriadoEm,
            Ativo = usuario.Ativo,
            PerfilId = usuario.PerfilId,
            Perfil = usuario.Perfil == null ? null : new PerfilViewModel
            {
                Id = usuario.Perfil.Id,
                Nome = usuario.Perfil.Nome
            },
            PessoaId = usuario.PessoaId,
            Pessoa = new PessoaFisicaViewModel
            {
                Id = usuario.Pessoa.Id,
                Nome = usuario.Pessoa.Nome,
                Email = usuario.Pessoa.Email,
                CPF = usuario.Pessoa.CPF,
                RG = usuario.Pessoa.RG,
                DataNascimento = usuario.Pessoa.DataNascimento
            }
        };

        return View(usuarioViewModel);
    }

    // POST: USUARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int? id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}
