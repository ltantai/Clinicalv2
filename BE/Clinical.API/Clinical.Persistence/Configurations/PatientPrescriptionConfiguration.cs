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
    public class PatientPrescriptionConfiguration : IEntityTypeConfiguration<PatientPrescription>
    {
        public void Configure(EntityTypeBuilder<PatientPrescription> builder)
        {
            builder.ToTable("PatientPrescriptions");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Patient)
                   .WithMany(x => x.PatientPrescriptions)
                   .HasForeignKey(x => x.PatientId);
        }
    }
}
