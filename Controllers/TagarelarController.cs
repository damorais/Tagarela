using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tagarela.Data;
using Tagarela.Models;

namespace Tagarela.Controllers
{
    public class TagarelarController : Controller
    {
        private readonly TagarelaContext _context;
        private readonly UserManager<User> _userManager;

        public TagarelarController(TagarelaContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioLogado =
                await _context
                    .Users.Include(user => user.Segue)
                    .FirstAsync(user => user.UserName == this._userManager.GetUserName(User));


            // var mensagens = _context.Mensagens.Include(m => m.Autor)
            //     .Where(mensagem => usuarioLogado.Segue.Contains(mensagem.Autor) || mensagem.Autor == usuarioLogado)
            //     .OrderByDescending(m => m.CriadoEm)
            //     .ToList();

            //SELECT ... FROM Mensagens WHERE IdAutor IN (...) OR IdAutor = 'damorais'
            var mensagens = from mensagem in _context.Mensagens.Include(m => m.Autor)
                            where usuarioLogado.Segue.Contains(mensagem.Autor) || usuarioLogado == mensagem.Autor
                            orderby mensagem.CriadoEm descending
                            select mensagem;

            return View(model: mensagens);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Conteudo,CriadoEm")] Mensagem mensagem)
        {
            var usuarioLogado = await _userManager.GetUserAsync(User);

            mensagem.Autor = usuarioLogado;

            _context.Add(mensagem);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}