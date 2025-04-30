using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Model;
using SistemaEscolarAPI.DTO;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SistemaEscolarAPI.Db;

namespace SistemaEscolarAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController (AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Curso)
                .Select(disciplinas => new DisciplinaDTO { 
                    Descricao = disciplinas.Descricao, 
                    Curso = disciplinas.Curso.Descricao
                })
                .ToListAsync();

            return Ok(disciplinas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO disciplinaDTO)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado");

            var disciplina = new Disciplina { Descricao = disciplinaDTO.Descricao, CursoId = Curso.Id };
            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDTO)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id); //  Vai fazer a procura do aluno, ou seja da enntidade pelo seu indificador
            if (disciplina == null) return NotFound("Disciplina não encontada");// Se caso si caso cair nesse condição  404
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encotrado"); //  Se caso caso cair nesse condição da erro 400

            disciplina.Descricao = disciplinaDTO.Descricao; // Aqui vai atualizar o ALuno no models e no DTO
            _context.Disciplinas.Update(disciplina); // Atualiza a disciplina no contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id); // Procura a disciplina pelo ID
            if (disciplina == null) return NotFound("Disciplina não encontrada"); // Se não encontrar, retorna 404

            _context.Disciplinas.Remove(disciplina); // Remove a disciplina do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content
        }
    }
}