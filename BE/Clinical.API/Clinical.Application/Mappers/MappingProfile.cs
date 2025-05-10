using AutoMapper;
using Clinical.Application.ViewModels;
using Clinical.Domain.Entitites;

namespace Clinical.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define your mappings here
            CreateMap<Patient, PatientViewModel>().ReverseMap();
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
            CreateMap<PatientPrescription, PatientPrescriptionViewModel>().ReverseMap();

            // Add more mappings as needed
        }
    }
}
