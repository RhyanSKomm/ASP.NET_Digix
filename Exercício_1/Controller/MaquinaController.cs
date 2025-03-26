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
    [Route("maquina")]
    public class MaquinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaquinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Maquina>> Get()
        {
            return await _context.Maquina.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Maquina>> Post([FromBody] Maquina maquina)
        {
            _context.Maquina.Add(maquina);
            await _context.SaveChangesAsync();
            return maquina;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Maquina>> Put(int id, [FromBody] Maquina maquina)
        {
            var existente = await _context.Maquina.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Tipo = maquina.Tipo;
            return existente;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Maquina.FindAsync(id);
            if (existente == null) return NotFound();
            _context.Maquina.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}