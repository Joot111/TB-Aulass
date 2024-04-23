using Aulas.Data;
using Aulas.Models.WebDTO;
using Microsoft.AspNetCore.Mvc;

namespace Aulas.Controllers.API
{
   /// <summary>
   /// Exemplo de inserção de dados à API
   /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        public ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            var listaDB = _context.Alunos.ToList();
            var listaRes = new List<TestDTO>(); 
            foreach (var item in listaDB)
            {
                TestDTO testDTO = new TestDTO();
                testDTO.None = item.None;
                testDTO.Nome = item.Nome;
                listaRes.Add(testDTO);
            }
            return Ok(listaRes);
        }

        [HttpPost]
        [Route("insereDado")]
        public ActionResult InsereDados(TestDTO testDTO)
        {
            testDTO.None = new TestDTO();
            testDTO.Nome = new TestDTO();

            _context.testDTO.Add(testDTO);
            _context.SaveChanges();

        }

        [Route("nome")]
        public IActionResult Ola(string nome, string turma)
        {
            if (turma != "A" && turma != "B" && turma != "C")
                return BadRequest();
            return Ok("Olá " + nome + "da turma " + turma);
        }
    }
}
