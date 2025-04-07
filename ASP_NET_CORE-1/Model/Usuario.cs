using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_CORE_1.Model
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]
        public int Id {get; set;}
        [Column("nome")]
        public string Nome {get; set;}
        [Column("email")]
        public string Email {get; set;}
    }
}