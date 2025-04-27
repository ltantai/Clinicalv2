using Clinical.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Persistence.Configurations
{
    public class PatientConfiguration: IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Doctor)
                   .WithMany(x => x.Patients)
                   .HasForeignKey(x => x.DoctorId);

            builder.HasMany(x => x.PatientPrescriptions)
                   .WithOne(x => x.Patient)
                   .HasForeignKey(x => x.PatientId);
        }
    }
}
