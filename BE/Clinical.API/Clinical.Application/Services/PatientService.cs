using AutoMapper;
using Clinical.Application.InputModels;
using Clinical.Application.InputModels.PaginationModels;
using Clinical.Application.Interfaces;
using Clinical.Application.Interfaces.GenericDataService;
using Clinical.Application.ViewModels;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.Services
{
    public class PatientService: IPatientService
    {
        private readonly IGenericDataService _repository;
        private readonly IMapper _mapper;
        public PatientService(
            IGenericDataService repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PatientViewModel>> GetAllActivePatientsWithDoctor()
        {
            var data = await _repository.GetAllAsync(
                condition: p => true,
                includeExpressions: new List<Expression<Func<Patient, object?>>>
                {
                    p => p.Doctor
                }
            );

            var result = _mapper.Map<List<PatientViewModel>>(data);
            return result;
        }

        public async Task<PaginatedResponse<PatientViewModel>> GetPaginatedPatientsAsync(Expression<Func<Patient, bool>> condition, int pageNumber, int pageSize)
        {
            var (patients, totalCount) = await _repository.GetAllPaginatedAsync(condition, pageNumber, pageSize);

            var patientDtos = _mapper.Map<List<PatientViewModel>>(patients);

            return new PaginatedResponse<PatientViewModel>
            {
                Items = patientDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task AddPatient(PatientInputModel patient)
        {
            var newPatient = new Patient();
            newPatient.PatientName = patient.PatientName;
            newPatient.Gender = patient.Gender;
            newPatient.Age = patient.Age;
            newPatient.Address = patient.Address;
            newPatient.LowerLevel = patient.LowerLevel;
            newPatient.MedicalTreatmentDepartment = patient.MedicalTreatmentDepartment;
            newPatient.TreatmentIndication = patient.TreatmentIndication;
            newPatient.DoctorId = patient.DoctorId;

            await _repository.AddAsync(newPatient);
            await _repository.SaveChangesAsync();
        }
    }
}
