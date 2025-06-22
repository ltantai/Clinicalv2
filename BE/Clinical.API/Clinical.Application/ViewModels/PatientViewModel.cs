using AutoMapper;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? Gender { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
        public string? MedicalTreatmentDepartment { get; set; }
        public string? LowerLevel { get; set; }
        public string? TreatmentIndication { get; set; }
        public string? Note { get; set; }
        public int DoctorId { get; set; }
        public DoctorViewModel? Doctor { get; set; }
        public ICollection<PatientPrescriptionViewModel>? PatientPrescriptions { get; set; }
    }

}
