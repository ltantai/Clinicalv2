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
    public interface IDoctorService
    {
        Task<DoctorViewModel> GetDoctorById(int id);
        Task<List<DoctorViewModel>> GetAll();
        Task<PaginatedResponse<DoctorViewModel>> GetPaginatedDoctorsAsync(string search, int pageNumber, int pageSize);
        Task Add(DoctorInputModel doctor);
        Task Update(DoctorInputModel doctor);
        Task Delete(int doctorId);
    }
}
