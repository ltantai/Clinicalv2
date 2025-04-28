using AutoMapper;
using Clinical.Application.InputModels;
using Clinical.Application.InputModels.PaginationModels;
using Clinical.Application.Interfaces;
using Clinical.Application.Interfaces.GenericDataService;
using Clinical.Application.ViewModels;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<PaginatedResponse<PatientViewModel>> GetPaginatedPatientsAsync(string search, int pageNumber, int pageSize)
        {
            search = search.Replace("\"", "").Trim();
            var (patients, totalCount) = await _repository.GetAllPaginatedAsync(
                x => !string.IsNullOrEmpty(x.PatientName) && x.PatientName.Contains(search),
                pageNumber,
                pageSize,
                new List<Expression<Func<Patient, object?>>>
                {
                    x => x.Doctor
                });

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
            newPatient.Age = patient.Age ?? 1;
            newPatient.Address = patient.Address;
            newPatient.LowerLevel = patient.LowerLevel;
            newPatient.MedicalTreatmentDepartment = patient.MedicalTreatmentDepartment;
            newPatient.TreatmentIndication = patient.TreatmentIndication;
            newPatient.DoctorId = patient.DoctorId;

            newPatient = await _repository.AddAsync(newPatient);

            if (patient.PatientPrescriptionInputModels != null && patient.PatientPrescriptionInputModels.Any())
            {
                var newPatientPrescriptions = new List<PatientPrescription>();
                var patientPrescription = (await _repository.GetAsync<PatientPrescription>(x => x.Order > 0)).OrderByDescending(o => o.Order).FirstOrDefault();
                var orderNum = patientPrescription != null ? patientPrescription.Order + 1 : 1;

                foreach (var item in patient.PatientPrescriptionInputModels)
                {
                    var newPatientPrescription = new PatientPrescription();
                    newPatientPrescription.MedicineName = item.MedicineName;
                    newPatientPrescription.NumberOfTimesPerDay = item.NumberOfTimesPerDay;
                    newPatientPrescription.NumberOfPillsPerDose = item.NumberOfPillsPerDose;
                    newPatientPrescription.Order = orderNum;
                    newPatientPrescription.PatientId = newPatient.Id;

                    newPatientPrescriptions.Add(newPatientPrescription);
                }

                if (newPatientPrescriptions.Any())
                {
                   await _repository.AddRangeAsync(newPatientPrescriptions);
                }
            }
        }

        public async Task UpdatePatient(PatientInputModel patient)
        {
            var existingPatient = await _repository.GetByIdAsync<Patient>(patient.Id ?? 0) ?? throw new Exception("Patient is not found to update");
            existingPatient.PatientName = patient.PatientName;
            existingPatient.Gender = patient.Gender;
            existingPatient.Age = patient.Age ?? 1;
            existingPatient.Address = patient.Address;
            existingPatient.LowerLevel = patient.LowerLevel;
            existingPatient.MedicalTreatmentDepartment = patient.MedicalTreatmentDepartment;
            existingPatient.TreatmentIndication = patient.TreatmentIndication;
            existingPatient.DoctorId = patient.DoctorId;

            await _repository.UpdateAsync(existingPatient);
        }

        public async Task DeletePatient(int patientId)
        {
            var existingPatient = await _repository.GetByIdAsync<Patient>(patientId) ?? throw new Exception("Patient is not found to delete");
            await _repository.DeleteAsync<Patient>(existingPatient.Id);
        }

        public async Task<PatientViewModel> GetPatientById(int id)
        {
            var patient = await _repository.GetByIdAsync(id, new List<Expression<Func<Patient, object?>>>
                {
                    p => p.PatientPrescriptions
                }) ?? throw new Exception("Patient detail is not found");
            var result = _mapper.Map<PatientViewModel>(patient);
            return result;
        }

        public async Task<List<PatientPrescriptionViewModel>> GetPrescriptionByTimeline(string dateFrom, string dateTo)
        {
            DateTime startDate = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = (DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Date.AddDays(1).AddTicks(-1);
            var data = await _repository.GetAllAsync<PatientPrescription>(condition: x => x.CreateTime >= startDate && endDate <= x.CreateTime);

            var result = _mapper.Map<List<PatientPrescriptionViewModel>>(data);
            return result;
        }
    }
}
