using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Model;
using SistemaEscolarAPI.DTO;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolarAPI.Db;
using Microsoft.VisualBasic;
using SistemaEscolarAPI.DTOs;

namespace SistemaEscolarAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController (AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        {
            var alunos = await _context.Alunos
                .Include(a=> a.Curso)
                .Select(aluno => new AlunoDTO {
                    Id = aluno.Id,
                    Nome = aluno.Nome,
                    Curso = aluno.Curso.Descricao
                })
                .ToListAsync();
            return Ok(alunos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDTO)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado");

            var aluno = new Aluno {
                Nome = alunoDTO.Nome,
                CursoId = Curso.Id
            };
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Aluno cadastrado com sucesso"});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] AlunoDTO alunoDTO)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest();

            aluno.Nome = alunoDTO.Nome;
            aluno.CursoId = Curso.Id;

            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Aluno cadastrado com sucesso"});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return Ok(new {mensagem = "Aluno cadastrado com sucesso"});
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<AlunoDTO>> GetById(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return NotFound();

            var alunoDTO = new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Curso = aluno.Curso.Descricao
            };
            return Ok(alunoDTO);
        }
    }
}