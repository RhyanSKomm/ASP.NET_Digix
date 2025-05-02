using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolarAPI.Db;
using SistemaEscolarAPI.DTO;
using SistemaEscolarAPI.Model;
using SistemaEscolarAPI.Services;


namespace SistemaEscolarAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.UserName) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return BadRequest("Usuario ou senha sao obrigatorios");
            }

            var users = new List<Usuario>
            {
                new Usuario { Username = "admin", Password = "123", Role = "Admin" },
                new Usuario { Username = "func", Password = "123", Role = "Funcionario" }
            };

            var user = users.FirstOrDefault( u => u.Username == loginDTO.UserName && u.Password == loginDTO.Password);

            if (user == null) return Unauthorized(new {message = "Usuario ou senha invalidos"});

            var token = TokenServices.GenerateToken(user);
            return Ok(new {token});        
        }
    }
}
