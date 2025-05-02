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

namespace SistemaEscolarAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaAlunoCursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> Get()
        {
            var regitros = await _context.DisciplinaAlunoCursos
            .Include(d => d.Aluno)
            .Include(d => d.Curso)
            .Include(d => d.Disciplina)
              .Select(d => new DisciplinaAlunoCursoDTO
              {
                  Id = d.AlunoId + d.CursoId + d.DisciplinaId,
                  AlunoId = d.AlunoId,
                  CursoId = d.CursoId,
                  DisciplinaId = d.DisciplinaId,
                  AlunoNome = d.Aluno.Nome,
                  CursoDescricao = d.Curso.Descricao,
                  DisciplinaDescricao = d.Disciplina.Descricao
              })
              .ToListAsync();


            return Ok(regitros);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var entidade = new DisciplinaAlunoCurso
            {
                AlunoId = disciplinaAlunoCursoDTO.AlunoId,
                CursoId = disciplinaAlunoCursoDTO.CursoId,
                DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId
            };
            _context.DisciplinaAlunoCursos.Add(entidade);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var entidade = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            entidade.AlunoId = disciplinaAlunoCursoDTO.AlunoId;
            entidade.CursoId = disciplinaAlunoCursoDTO.CursoId;
            entidade.DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId;

            _context.DisciplinaAlunoCursos.Update(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entidade = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            _context.DisciplinaAlunoCursos.Remove(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaAlunoCursoDTO>> GetById(int id)
        {
            var entidade = await _context.DisciplinaAlunoCursos
                .Include(d => d.Aluno)
                .Include(d => d.Curso)
                .Include(d => d.Disciplina)
                .FirstOrDefaultAsync(d => (d.AlunoId + d.CursoId + d.DisciplinaId) == id);

            if (entidade == null)
            {
                return NotFound();
            }

            var dto = new DisciplinaAlunoCursoDTO
            {
                Id = entidade.AlunoId + entidade.CursoId + entidade.DisciplinaId,
                AlunoId = entidade.AlunoId,
                CursoId = entidade.CursoId,
                DisciplinaId = entidade.DisciplinaId,
                AlunoNome = entidade.Aluno.Nome,
                CursoDescricao = entidade.Curso.Descricao,
                DisciplinaDescricao = entidade.Disciplina.Descricao
            };

            return Ok(dto);
        }

    }
}