using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_1.Model
{
    [Table("maquina")]
    public class Maquina
    {
        [Key]
        [Column("id_maquina")]
        public int Id { get; set; }
        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;
        [Column("velocidade")]
        public int Velocidade { get; set; }
        [Column("harddisk")]
        public int Hardidisk { get; set; }
        [Column("placa_rede")]
        public int Placa_rede { get; set; }
        [Column("memoria_ram")]
        public int Memoria_ram { get; set; }
        [ForeignKey("usuarios")]
        [Column("fk_usuario")]
        public int Fk_usuario { get; set; }
    }
}