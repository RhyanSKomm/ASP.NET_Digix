using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_1.Model
{
    [Table("software")]

    public class Software
    {
        [Key]
        [Column("id_software")]
        public int Id { get; set; }
        [Column("produto")]
        public string Produto { get; set; } = string.Empty;
        [Column("harddisk")]
        public int Hardidisk { get; set; }
        [Column("memoria_ram")]
        public int Memoria_ram { get; set; }
        [ForeignKey("maquina")]
        [Column("fk_maquina")]
        public int Fk_maquina { get; set; }
    }
}