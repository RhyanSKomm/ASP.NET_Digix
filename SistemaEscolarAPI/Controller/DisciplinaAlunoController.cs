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
              .Select(d => new DisciplinaAlunoCursoDTO
              {
                  AlunoId = d.AlunoId,
                  CursoId = d.CursoId,
                  DisciplinaId = d.DisciplinaId,
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
    }
}