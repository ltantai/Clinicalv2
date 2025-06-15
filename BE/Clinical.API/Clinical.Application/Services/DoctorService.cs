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
    public class DoctorService : IDoctorService
    {
        private readonly IGenericDataService _repository;
        private readonly IMapper _mapper;
        public DoctorService(
            IGenericDataService repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DoctorViewModel>> GetAll()
        {
            var data = await _repository.GetAllAsync<Doctor>();

            var result = _mapper.Map<List<DoctorViewModel>>(data);
            return result;
        }

        public async Task<PaginatedResponse<DoctorViewModel>> GetPaginatedDoctorsAsync(string search, int pageNumber, int pageSize)
        {
            search = search.Replace("\"", "").Trim();
            var (doctors, totalCount) = await _repository.GetAllPaginatedAsync<Doctor>(
                x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(search),
                pageNumber,
                pageSize
               );

            var doctorDtos = _mapper.Map<List<DoctorViewModel>>(doctors);

            return new PaginatedResponse<DoctorViewModel>
            {
                Items = doctorDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task Add(DoctorInputModel doctor)
        {
            try
            {
                var newDoctor = new Doctor();
                newDoctor.Name = doctor.Name;
                newDoctor.Description = doctor.Description;
                newDoctor.PhoneNumber = doctor.PhoneNumber;
                newDoctor.Email = doctor.Email;

                await _repository.AddAsync(newDoctor);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task Update(DoctorInputModel doctor)
        {
            try
            {
                var existingDoctor = await _repository.GetByIdAsync<Doctor>(doctor.Id) ?? throw new Exception("Doctor is not found to update");
                existingDoctor.Name = doctor.Name;
                existingDoctor.Description = doctor.Description;
                existingDoctor.PhoneNumber = doctor.PhoneNumber;
                existingDoctor.Email = doctor.Email;

                await _repository.UpdateAsync(existingDoctor);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task Delete(int doctorId)
        {
            var existingDoctor = await _repository.GetByIdAsync<Doctor>(doctorId) ?? throw new Exception("Doctor is not found to delete");
            await _repository.DeleteAsync<Doctor>(existingDoctor.Id);
        }

        public async Task<DoctorViewModel> GetDoctorById(int id)
        {
            var doctor = await _repository.GetByIdAsync<Doctor>(id) ?? throw new Exception("Doctor detail is not found");
            var result = _mapper.Map<DoctorViewModel>(doctor);
            return result;
        }
    }
}
