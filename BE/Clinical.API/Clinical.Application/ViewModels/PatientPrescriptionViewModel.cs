using AutoMapper;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.ViewModels
{
    public class PatientPrescriptionViewModel
    {
        public int Id { get; set; }
        public string? MedicineName { get; set; }
        public int? NumberOfTimesPerDay { get; set; }
        public int? NumberOfPillsPerDose { get; set; }
        public int Order { get; set; }
        public string? Note { get; set; }
        public int PatientId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
