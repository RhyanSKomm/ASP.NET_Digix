using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Model
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }
    }
}