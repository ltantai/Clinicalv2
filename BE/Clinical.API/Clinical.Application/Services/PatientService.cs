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
        private readonly IGenericDataService _dataService;

        public PatientService(IGenericDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<PatientViewModel>> GetAllActivePatientsWithDoctor()
        {
            var data = await _dataService.GetAllAsync(
                condition: p => true,
                includeExpressions: new List<Expression<Func<Patient, object?>>>
                {
                    p => p.Doctor
                }
            );

            var result = new List<PatientViewModel>();
            if (data.Any())
            {
                foreach (var item in data)
                {
                    var newPatient = new PatientViewModel();
                    newPatient.PatientName = item.PatientName;
                    newPatient.Address = item.Address;
                    newPatient.MedicalTreatmentDepartment = item.MedicalTreatmentDepartment;

                    result.Add(newPatient);
                }
            }

            return result;
        }

        public async Task AddPatient(Patient patient)
        {
            await _dataService.AddAsync(patient);
            await _dataService.SaveChangesAsync();
        }
    }
}
