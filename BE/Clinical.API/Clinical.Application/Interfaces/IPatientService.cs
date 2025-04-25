using Clinical.Application.InputModels;
using Clinical.Application.InputModels.PaginationModels;
using Clinical.Application.ViewModels;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientViewModel>> GetAllActivePatientsWithDoctor();
        Task<PaginatedResponse<PatientViewModel>> GetPaginatedPatientsAsync( Expression<Func<Patient, bool>> condition, int pageNumber, int pageSize);
        Task AddPatient(PatientInputModel patient);
    }
}
