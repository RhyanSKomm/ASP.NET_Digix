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
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController (AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> Get()
        {
            var cursos = await _context.Cursos
                .Select(curso => new CursoDTO {
                    Id = curso.Id,
                    Descricao = curso.Descricao,
                })
                .ToListAsync();
            return Ok(cursos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CursoDTO cursoDTO)
        {
            var curso = new Curso {
                Descricao = cursoDTO.Descricao,
                
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] CursoDTO cursoDTO)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            curso.Descricao = cursoDTO.Descricao;

            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<CursoDTO>> GetById(int id)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null) return NotFound();
            var cursoDTO = new CursoDTO
            {
                Id = curso.Id,
                Descricao = curso.Descricao,
            };
            return Ok(cursoDTO);
        }
    }
}