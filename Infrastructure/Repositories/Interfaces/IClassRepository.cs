﻿using adstaskhub_api.Application.DTOs;
using adstaskhub_api.Domain.Models;

namespace adstaskhub_api.Infrastructure.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<List<ClassDTO>> GetAllClassesDTO();
        Task<Class> GetClassById(long id);
        Task<ClassDTO> GetClassDTOById(long id);
        Task<Class> GetClassByClassNumberAndPeriod(int classNumber, int periodNumber);
        Task<ClassDTO> CreateClass(Class @class);
        Task<ClassDTO> UpdateClass(Class @class, long id);
        Task<bool> DeleteClass(long id);
        Task<bool> SoftDeleteClass(long id);
    }
}
