using Clinical.Application.ViewModels;
using Clinical.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientViewModel>> GetAllActivePatientsWithDoctor();
    }
}
