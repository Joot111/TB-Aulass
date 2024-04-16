using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aulas.Data;
using Aulas.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aulas.Controllers
{
    public class CursosController : Controller
    {
        /// <summary>
        /// referência à BD do projeto
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// objeto que contêm os dados referentes ao ambiente do Servidor
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CursosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
            /* Algoriitmo
              1 - há ficheiros?
                1.1 - não há ficheiro:
                    devolver à view dizendo que o ficheiro é obrigatório
                1.2 - há ficheiro:
                    Mas é uma imagem (PNG, JPG)?
                    1.2.1 - não é PNG nem JPG
                            - devolver o controle à view e pedir PNG ou JPG
                    1.2.2 - é uma imagem
                            - determinar o nome a atribuir ao ficheiro
                            - escrever esse nome nome na BD
                            - se a escrita na BD se concretizar, é que o ficheiro é guardado no disco rígido
             */

            // variáveis auxiliares
            string nomeImagem = "";
            bool haImagem = false;

            // há ficheiro?
            if(ImagemLogo == null)
            {
                ModelState.AddModelError("", "O fornecimento de um Logotipo é obrigatírio");
                return View(cursos);
            } else
            {
                // há ficheiros, mas é imagem?
                if (!(ImagemLogo.ContentType == "imagem/png" || ImagemLogo.ContentType == "imagem/jpg"))
                {
                    ModelState.AddModelError("", "Tem que fornecer um logotipo do tipo PNG ou JPG");
                    return View(cursos);
                }
                else
                {
                    haImagem = true;
                    // há ficheiros . e é uma imagem válida
                    Guid g = Guid.NewGuid();
                    nomeImagem = g.ToString();
                    // obter a extensão do nome do ficheiro
                    string extensao = Path.GetExtension(ImagemLogo.FileName);
                    // adcionar a exetensão ao nome da imagem
                    nomeImagem += extensao;
                    // adicionar o nome do ficheiro ao objeto que guarda
                    // vem do browser
                    cursos.Logotipo = nomeImagem;
                }
            }

            // avalia se os dados que chegam da View estão de acordo com o Model
            if (ModelState.IsValid)
            {
                // adiciona os dados vindos da View à BD
                _context.Add(cursos);
                // efetua um COMMIT na BD
                await _context.SaveChangesAsync();

                // se há fichiro de imagem, vamos guardar no ficheiro disco rígido do servidor
                if (haImagem)
                {
                    string nomePastaOndeGuardarImagem = _webHostEnvironment.WebRootPath;
                    // já sei o caminho até à pasta wwroot específico onde vou guardar a imagem
                    nomePastaOndeGuardarImagem = Path.Combine(nomePastaOndeGuardarImagem,"Imagens");
                    // e, existe a pasta 'Imagens' ?
                    if (Directory.Exists(nomePastaOndeGuardarImagem))
                    {
                        Directory.CreateDirectory(nomePastaOndeGuardarImagem);
                    }
                    // juntar o nome do ficheiro à sua localização
                    string nomeFinalDaImagem = Path.Combine(nomePastaOndeGuardarImagem, nomeImagem);
                    
                    // guardar a imagem no disco rígido
                    using var stream = new FileStream(nomeFinalDaImagem, FileMode.Create);
                    await ImagemLogo.CopyToAsync(stream);
                }

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
