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
    [Route("software")]
    public class SoftwareController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SoftwareController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Software>> Get()
        {
            return await _context.Software.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Software>> Post([FromBody] Software software)
        {
            _context.Software.Add(software);
            await _context.SaveChangesAsync();
            return software;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Software>> Put(int id, [FromBody] Software software)
        {
            var existente = await _context.Software.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Produto = software.Produto;
            return existente;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Software.FindAsync(id);
            if (existente == null) return NotFound();
            _context.Software.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}