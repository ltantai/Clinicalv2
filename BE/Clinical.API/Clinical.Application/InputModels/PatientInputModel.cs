﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.InputModels
{
    public class PatientInputModel
    {
        public int? Id { get; set; }
        public string? PatientName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? LowerLevel { get; set; }
        public string? MedicalTreatmentDepartment { get; set; }
        public string? TreatmentIndication { get; set; }
        public int DoctorId { get; set; }
        public string? Note { get; set; }
        public ICollection<PatientPrescriptionInputModel>? PatientPrescriptionInputModels { get; set; }
    }
}
