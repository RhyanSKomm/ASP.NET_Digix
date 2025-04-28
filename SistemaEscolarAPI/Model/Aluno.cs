using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Model
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CursoId { get; set; }
        public Curso Curso { get; set; }
        
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; } = new List<DisciplinaAlunoCurso>();
    }
}