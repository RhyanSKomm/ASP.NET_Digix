using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Model;


namespace SistemaEscolarAPI.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasKey(dac => new { dac.AlunoId, dac.DisciplinaId, dac.CursoId });

            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(dac => dac.Aluno)
                .WithMany(a => a.DisciplinaAlunoCursos)
                .HasForeignKey(dac => dac.AlunoId);

            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(dac => dac.Disciplina)
                .WithMany(d => d.DisciplinaAlunoCursos)
                .HasForeignKey(dac => dac.DisciplinaId);

            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(dac => dac.Curso)
                .WithMany(c => c.DisciplinaAlunoCursos)
                .HasForeignKey(dac => dac.CursoId);

        }
    }
}