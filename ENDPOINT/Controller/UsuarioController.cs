using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENDPOINT.Model;
using Microsoft.AspNetCore.Mvc;

namespace ENDPOINT.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario() {Id = 1, Nome = "João", Email = "joao@gmail.com"},
            new Usuario() {Id = 1, Nome = "Maria", Email = "maria@gmail.com"},
            new Usuario() {Id = 1, Nome = "José", Email = "jose@gmail.com"}
        };

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return usuarios;
        }

        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario);
            return usuario;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            var usuarioExistente = usuarios.Where(x => x.Id == id).FirstOrDefault();
            if(usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email; 
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usuarioExistente = usuarios.Where(x=>x.Id == id).FirstOrDefault();
            if(usuarioExistente != null)
            {
                usuarios.Remove(usuarioExistente);
            }
        }
    }
}