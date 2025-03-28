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
        [Key] // Define a chave primária
        [Column("id_Maquina")]
        public int Id { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("velocidade")]
        public int Velocidade { get; set; }

        [Column("hardDisk")]
        public int HardDisk { get; set; }

        [Column("placa_Rede")]
        public int PlacaRede { get; set; }

        [Column("memoria_Ram")]
        public int MemoriaRam { get; set; }

        [ForeignKey("Usuario")]
        [Column("fk_usuario")]
        public int FkUsuario { get; set; }
    }
}