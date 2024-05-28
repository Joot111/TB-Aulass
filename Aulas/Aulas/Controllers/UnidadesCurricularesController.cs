using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aulas.Data;
using Aulas.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Aulas.Controllers
{
    [Authorize]
    public class UnidadesCurricularesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadesCurricularesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnidadesCurriculares
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UCs.Include(u => u.Curso);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UnidadesCurriculares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // procura os dados da UC:
            // em SQL: Select * From Unidades Curriculares uc
            //         Inner Join Cursos c On uc.CursoFK = uc.Id
            //         Inner Join ProfessoresUnidadesCurriculares pc On puc. UCFK = uc.Id
            //         Inner Join Professores p On pc.ProfFK = p.Id
            //         Where uc.Id = id
            // em LINQ 
            var unidadeCurricular = await _context.UCs
                                                  .Include(u => u.Curso)
                                                  .Include(p => p.ListaProfessores)
                                                  .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            return View(unidadeCurricular);
        }

        // GET: UnidadesCurriculares/Create
        public IActionResult Create()
        {
            // id --> value associado á dropdown
            // nome --> atributo nome associado ao curso
            // procurar os dados apresentados na 'dropdown' dos Cursos
            ViewData["CursoFK"] = new SelectList(_context.Cursos.OrderBy(c=>c.Nome), "Id", "Nome");

            // obter a lista de professores,
            // para enviar para a View

            // em SQL: Select * From Professores p Order By p.nome
            // LINQ:
            var listaProfs = _context.Professores.OrderBy(p=>p.Nome).ToList();
            ViewData["listaProfessores"] = listaProfs;
            return View();
        }

        // POST: UnidadesCurriculares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// recolha de dados na adição de uma nova Unidade Curricular
        /// </summary>
        /// <param name="unidadeCurricular">dados da Unidade Curricular</param>
        /// <param name="escolhaProfessores">lista dos IDs dos professores são recolhidos para a criação de uma UC</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,AnoCurricular,Semestre,CursoFK")] UnidadesCurriculares unidadeCurricular, int[] escolhaProfessores)
        {
            // var. auxiliar
            bool haErros = false;

            // Validações
            if(escolhaProfessores.Length == 0)
            {
                // não escolhi nenhum professor
                ModelState.AddModelError("", "Escolha um Professor, por favor.");
                haErros = true;
            }

            if(unidadeCurricular.CursoFK == -1)
            {
                // não foi selecionado nenhum curso
                ModelState.AddModelError("", "Escolha um Curso, por favor.");
                haErros = true;
            }
            

            if (ModelState.IsValid && !haErros)
            {
                // associar os professores escolhidos à UC
                // criar uma Lista Professores
                var listaProfsNaUC = new List<Professores>();
                foreach(var prof in escolhaProfessores)
                {
                    // procurar o Professor na BD
                    var p = await _context.Professores.FindAsync(prof);
                    if(p != null)
                    {
                        listaProfsNaUC.Add(p);
                    }
                }
                // atribuir a Lista de Professores à UC
                unidadeCurricular.ListaProfessores = listaProfsNaUC;

                _context.Add(unidadeCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // se chegar aqui, é porque houve problemas
            // vou devolver o controlo à View
            // Tenho de preparar os dados para enviar
            ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Nome", unidadeCurricular.CursoFK);
            var listaProfs = _context.Professores.OrderBy(p => p.Nome).ToList();
            ViewData["listaProfessores"] = listaProfs;
            return View(unidadeCurricular);
        }

        // GET: UnidadesCurriculares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadesCurriculares = await _context.UCs.FindAsync(id);
            if (unidadesCurriculares == null)
            {
                return NotFound();
            }
            ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", unidadesCurriculares.CursoFK);
            return View(unidadesCurriculares);
        }

        // POST: UnidadesCurriculares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,AnoCurricular,Semestre,CursoFK")] UnidadesCurriculares unidadesCurriculares)
        {
            if (id != unidadesCurriculares.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadesCurriculares);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadesCurricularesExists(unidadesCurriculares.Id))
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
            ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", unidadesCurriculares.CursoFK);
            return View(unidadesCurriculares);
        }

        // GET: UnidadesCurriculares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadesCurriculares = await _context.UCs
                .Include(u => u.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadesCurriculares == null)
            {
                return NotFound();
            }

            return View(unidadesCurriculares);
        }

        // POST: UnidadesCurriculares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadesCurriculares = await _context.UCs.FindAsync(id);
            if (unidadesCurriculares != null)
            {
                _context.UCs.Remove(unidadesCurriculares);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadesCurricularesExists(int id)
        {
            return _context.UCs.Any(e => e.Id == id);
        }
    }
}
