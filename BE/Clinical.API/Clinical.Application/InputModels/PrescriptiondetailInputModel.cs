using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.InputModels
{
    public class PrescriptiondetailInputModel
    {
        public int PatientId { get; set; }
        public string? Note { get; set; }
        public ICollection<PatientPrescriptionInputModel>? PatientPrescriptionInputModels { get; set; }
    }
}
