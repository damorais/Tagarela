using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tagarela.Data;
using Tagarela.Models;

namespace Tagarela.Controllers
{
    [Route("Perfil")]
    public class PerfilController : Controller
    {
        private TagarelaContext _context;
        private UserManager<User> _userManager;

        public PerfilController(TagarelaContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [Route("{userName}")]
        public async Task<ActionResult> Show(string userName)
        {
            var usuarioPerfil = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            if (usuarioPerfil != null)
            {
                return View(model: usuarioPerfil);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("{userName}/Seguir")]
        public async Task<IActionResult> Seguir(string userName)
        {
            var usuarioLogado = await _context.Users.Include(user => user.Segue).FirstAsync(user => user.UserName == this._userManager.GetUserName(User));
            var usuarioASeguir = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

            if (usuarioASeguir != null)
            {
                usuarioLogado.Segue.Add(usuarioASeguir);
                _context.Update(usuarioLogado);

                await _context.SaveChangesAsync();

                return RedirectToAction(actionName: "Index", controllerName: "Tagarelar");
            }
            else
            {
                return NotFound();
            }
        }
    }
}