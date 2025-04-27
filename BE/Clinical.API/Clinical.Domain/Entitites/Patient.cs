using Clinical.Domain.Commons.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Domain.Entitites
{
    public class Patient : BaseEntity
    {
        public string? PatientName { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? LowerLevel { get; set; }
        public string? MedicalTreatmentDepartment { get; set; }
        public string? TreatmentIndication { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<PatientPrescription>? PatientPrescriptions { get; set; }
    }
}
