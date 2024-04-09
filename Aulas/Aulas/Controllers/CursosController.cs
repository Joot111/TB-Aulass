﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aulas.Data;
using Aulas.Models;

namespace Aulas.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursos == null)
            {
                return NotFound();
            }

            return View(cursos);
        }

        // GET: Cursos/Create
        // facultativo, pois esta função, por predfenição já reage ao GET
        public IActionResult Create()
            // a única ação desta função é mostrar a View
        {
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Cursos cursos, IFormFile ImagemLogo )
        {
            // avalia se os dados que chegam da View estão de acordo com o Model
            if (ModelState.IsValid)
            {
                // adiciona os dados vindos da View à BD
                _context.Add(cursos);
                // efetua um COMMIT na BD
                await _context.SaveChangesAsync();
                // redireciona o utilizador para a página Index
                return RedirectToAction(nameof(Index));
            }
            // se cheguei aqui é pq alguma coisa correu mal, volta à View com dados datualizados
            return View(cursos);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }
            return View(cursos);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Logotipo")] Cursos cursos)
        {
            if (id != cursos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosExists(cursos.Id))
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
            return View(cursos);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursos == null)
            {
                return NotFound();
            }

            return View(cursos);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursos = await _context.Cursos.FindAsync(id);
            if (cursos != null)
            {
                _context.Cursos.Remove(cursos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
