using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercício_1.Model;
using Microsoft.EntityFrameworkCore;

namespace Exercício_1.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Model.Maquina> Maquina {get; set;}
        public DbSet<Model.Software> Software {get; set;}
        public DbSet<Model.Usuarios> Usuarios {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().ToTable("usuarios");
            modelBuilder.Entity<Maquina>().ToTable("maquina");
            modelBuilder.Entity<Software>().ToTable("software");
        }
    }
}