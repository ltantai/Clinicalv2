using Clinical.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Persistence.DBContext
{
    public class ClinicalDbContext : DbContext
    {
        public ClinicalDbContext(DbContextOptions<ClinicalDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Patient> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
