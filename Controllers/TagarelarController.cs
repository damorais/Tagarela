using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public TagarelarController(TagarelaContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mensagens = _context.Mensagem
                                .OrderByDescending(m => m.CriadoEm)
                                .ToList();
            

            return View(model: mensagens);
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,Conteudo,CriadoEm")] Mensagem mensagem)
        {
            _context.Add(mensagem);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}