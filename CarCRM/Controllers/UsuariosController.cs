using CarCRM.Data;
using CarCRM.Models;
using CarCRM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[Authorize]
public class UsuariosController : Controller
{
    private readonly CarCRMContext _context;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UsuariosController(CarCRMContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //GET: Usuarios/Login
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    //POST: Usuarios/Login
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var usuario = await _userManager.FindByEmailAsync(model.Email);

        if (usuario == null || !usuario.Ativo)
        {
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            return View(model);
        }

        var resultado = await _signInManager.PasswordSignInAsync(usuario, model.Senha, model.LembrarMe,
            lockoutOnFailure: true);

        if (resultado.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        if (resultado.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas!");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Email ou Senha inválidos");
        }

        return View(model);

    }

    // POST: Usuarios/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    private async Task CarregarPerfisAsync()
    {
        ViewBag.Perfis = await _context.Roles.OrderBy(r => r.Id).ToListAsync();
    }

    // GET: USUARIOS
    public async Task<IActionResult> Index()    
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Pessoa)
            .ToListAsync();

        var usuariosViewModel = usuarios.Select(u => new UsuarioViewModel
        {
            Id = u.Id,
            Nome = u.Nome,
            //CriadoEm = u.CriadoEm,
            Ativo = u.Ativo,
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
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
       .Include(u => u.Pessoa)
       .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Ativo = usuario.Ativo,
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
    public async Task<IActionResult> Create()
    {
        await CarregarPerfisAsync();
        var usuarioViewModel = new UsuarioViewModel();
        return View(usuarioViewModel);
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
            //var usuario = new Usuario
            //{
            //    Nome = usuarioViewModel.Nome,
            //    Ativo = usuarioViewModel.Ativo,

            //    Pessoa = new PessoaFisica
            //    {
            //        Nome = usuarioViewModel.Nome,
            //        Email = usuarioViewModel.Pessoa.Email,
            //        CPF = usuarioViewModel.Pessoa.CPF,
            //        RG = usuarioViewModel.Pessoa.RG,
            //        DataNascimento = usuarioViewModel.Pessoa.DataNascimento,
            //    }
            //};

            //_context.Add(usuario);

            //_context.UserRoles.Add(new IdentityUserRole<string>
            //{
            //    UserId = usuario.Id,
            //    RoleId = usuarioViewModel.PerfilId
            //});
            //await _context.SaveChangesAsync();
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == usuarioViewModel.PerfilId);
            if (role == null)
            {
                ModelState.AddModelError(nameof(usuarioViewModel.PerfilId), "Perfil inválido.");
                await CarregarPerfisAsync();
                return View(usuarioViewModel);
            }

            var user = new Usuario
            {
                UserName = usuarioViewModel.Pessoa.Email.Trim(),
                Email = usuarioViewModel.Pessoa.Email.Trim(),
                Nome = usuarioViewModel.Nome.Trim(),
                Ativo = true,
                Pessoa = new PessoaFisica
                {
                    Nome = usuarioViewModel.Nome,
                    Email = usuarioViewModel.Pessoa.Email,
                    CPF = usuarioViewModel.Pessoa.CPF,
                    RG = usuarioViewModel.Pessoa.RG,
                    DataNascimento = usuarioViewModel.Pessoa.DataNascimento,
                }
            };

            var result = await _userManager.CreateAsync(
                user, usuarioViewModel.Senha);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                await CarregarPerfisAsync();
                return View(usuarioViewModel);
            }
            
            var roleResult = await _userManager.AddToRoleAsync(
            user, role.Name);

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                // O usuário já foi criado; tratar a falha adequadamente.
                await CarregarPerfisAsync();
                return View(usuarioViewModel);
            }
            
            TempData["Sucesso"] = "Usuário cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        await CarregarPerfisAsync();
        return View(usuarioViewModel);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
        .Include(u => u.Pessoa)
        .FirstOrDefaultAsync(u => u.Id == id);
  
        if (usuario == null)
        {
            return NotFound();
        }


        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            CriadoEm = usuario.Pessoa.CriadoEm,
            Ativo = usuario.Ativo,
            PessoaId = usuario.PessoaId,
            PerfilId = await _context.UserRoles
            .Where(ur => ur.UserId == usuario.Id)
            .Select(ur => ur.RoleId)
            .FirstOrDefaultAsync(),
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
        await CarregarPerfisAsync();
        return View(usuarioViewModel);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UsuarioViewModel usuarioViewModel)
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
                usuario.Pessoa.Nome = usuarioViewModel.Nome;
                usuario.Pessoa.DataNascimento = usuarioViewModel.Pessoa.DataNascimento;

                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == usuarioViewModel.PerfilId);
                if (role == null)
                {
                    ModelState.AddModelError(nameof(usuarioViewModel.PerfilId), "Perfil inválido.");
                    await CarregarPerfisAsync();
                    return View(usuarioViewModel);
                }

                _context.Update(usuario);
                await _context.SaveChangesAsync();

                var perfisAtuais = await _userManager.GetRolesAsync(usuario);

                if (perfisAtuais.Count != 1 || perfisAtuais[0] != role.Name)
                {
                    await _userManager.RemoveFromRolesAsync(usuario, perfisAtuais);
                    var roleResult = await _userManager.AddToRoleAsync(usuario, role.Name);

                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                        await CarregarPerfisAsync();
                        return View(usuarioViewModel);
                    }
                }

                if (!string.IsNullOrWhiteSpace(usuarioViewModel.Senha))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var senhaResult = await _userManager.ResetPasswordAsync(usuario, token, usuarioViewModel.Senha);

                    if (!senhaResult.Succeeded)
                    {
                        foreach (var error in senhaResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                        await CarregarPerfisAsync();
                        return View(usuarioViewModel);
                    }
                }
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
            TempData["Sucesso"] = "Usuário atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        await CarregarPerfisAsync();
        return View(usuarioViewModel);
    }

    // GET: USUARIOS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
       .Include(u => u.Pessoa)
       .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioViewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            //CriadoEm = usuario.CriadoEm,
            Ativo = usuario.Ativo,
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
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(string id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}
