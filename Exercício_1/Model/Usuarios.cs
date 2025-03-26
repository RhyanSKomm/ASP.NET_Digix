using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_1.Model
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }
        [Column("password")]
        public string Password_Usuario { get; set; } = string.Empty;
        [Column("nome_usuario")]
        public string Nome_Usuario { get; set; } = string.Empty;
        [Column("ramal")]
        public int Ramal_Usuario { get; set; }
        [Column("especialidade")]
        public string Especialidade_Usuario { get; set; } = string.Empty;
    }
}