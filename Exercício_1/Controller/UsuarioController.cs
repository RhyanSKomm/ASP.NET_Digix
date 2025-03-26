using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Exercício_1.Model;
using Exercício_1.database;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Exercício_1.Controller
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Usuarios>> Get()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Usuarios>> Post([FromBody] Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuarios>> Put(int id, [FromBody] Usuarios usuario)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nome_Usuario = usuario.Nome_Usuario;
            return existente;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return NotFound();
            _context.Usuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}