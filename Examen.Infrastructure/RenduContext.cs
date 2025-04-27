using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;

namespace Examen.Infrastructure
{
    public class RenduContext:DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Bilan> Bilans { get; set; }
        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Analyse> Analyses { get; set; }

        //OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                        Initial Catalog=LaboMohamedNaceurKhlifi;
                                        Integrated Security=true;
                                        MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        // OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping Localisation -> AdresseLabo
            modelBuilder.Entity<Laboratoire>()
                .Property(l => l.Localisation)
                .HasColumnName("AdresseLabo")
                .HasMaxLength(50);

            // Appliquer la configuration de Bilan
            modelBuilder.ApplyConfiguration(new BilanConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
