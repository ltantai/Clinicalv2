using Clinical.Domain.Commons.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Domain.Entitites
{
    public class PatientPrescription : BaseEntity
    {
        public string? MedicineName { get; set; }
        public int? NumberOfTimesPerDay { get; set; }
        public int? NumberOfPillsPerDose { get; set; }
        public int Order { get; set; }
        public string? Note { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
