using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tutorias_Unphu.Models;

namespace Tutorias_Unphu.Controllers
{
    public class LoginController : Controller
    {
        private readonly TutoriasUnphuContext _context;

        public LoginController(TutoriasUnphuContext context)
        {
            _context = context;
        }
        // Vista
        public IActionResult Index()
        {
            return View();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> datos = _context.Usuarios.FromSqlRaw("Select * from usuarios").ToList();
            return datos;
        }
        public Usuario ValidarUsuario(string matricula, string password)
        {
            return ObtenerUsuarios().Where(x => x.Matricula == matricula && x.Password == password).FirstOrDefault();
        }

        public async Task<IActionResult> ValidarInicio(Usuario _usuario)
        {
            var usuario = ValidarUsuario(_usuario.Matricula, _usuario.Password);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Matricula)
                };

                string[] usuarioRol = { usuario.Rol };

                // aqui se aplican los roles
                foreach (string rol in usuarioRol)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                switch (usuario.Rol)
                {
                    case "administrador":
                        var claimsIdentityAdmin = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAdmin));
                        return RedirectToAction("Index", "AdminTutorias");
                        break;

                    case "estudiante":
                        var claimsIdentityEstudiante = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityEstudiante));
                        return RedirectToAction("Index", "TutoriasUnphu");
                        break;
                }

                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
