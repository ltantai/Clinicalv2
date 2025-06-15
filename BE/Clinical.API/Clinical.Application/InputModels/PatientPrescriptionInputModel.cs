using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.InputModels
{
    public class PatientPrescriptionInputModel
    {
        public int? Id { get; set; }
        public string? MedicineName { get; set; }
        public int? NumberOfTimesPerDay { get; set; }
        public int? NumberOfPillsPerDose { get; set; }
        public int Order { get; set; }
        public int PatientId { get; set; }
    }
}
