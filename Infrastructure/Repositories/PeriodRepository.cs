﻿using adstaskhub_api.Application.DTOs;
using adstaskhub_api.Domain.Models;
using adstaskhub_api.Infrastructure.Mappers;
using adstaskhub_api.Infrastructure.Mappers.Interfaces;
using adstaskhub_api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace adstaskhub_api.Infrastructure.Repositories
{
    public class PeriodRepository : IPeriodRepository
    {
        private readonly DBContext _dbContext;
        private readonly IPeriodMapper _periodMapper;

        public PeriodRepository(DBContext DBContext, IPeriodMapper periodMapper)
        {
            _dbContext = DBContext;
            _periodMapper = periodMapper;
        }

        public async Task<Period> GetPeriodById(long id)
        {
            return await _dbContext.Periods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PeriodDTO> GetPeriodDTOById(long id)
        {
            Period period = await _dbContext.Periods.FirstOrDefaultAsync(x => x.Id == id);
            return _periodMapper.MapToDTO(period);
        }

        public async Task<List<PeriodDTO>> GetAllPeriodsDTO()
        {
            List<Period> periods = await _dbContext.Periods.ToListAsync();

            return periods.Select(period => _periodMapper.MapToDTO(period)).ToList();
        }

        public async Task<PeriodDTO> CreatePeriod(Period period)
        {
            await _dbContext.Periods.AddAsync(period);
            await _dbContext.SaveChangesAsync();

            return _periodMapper.MapToDTO(period);
        }

        public async Task<PeriodDTO> UpdatePeriod(Period period, long id)
        {
            Period periodById = await GetPeriodById(id) ?? throw new Exception($"Period for ID: {id} not found");
            periodById.Id = period.Id;
            periodById.Number = period.Number;
            periodById.UpdatedAt = DateTime.UtcNow;

            _dbContext.Periods.Update(periodById);
            await _dbContext.SaveChangesAsync();
            return _periodMapper.MapToDTO(periodById);
        }

        public async Task<bool> DeletePeriod(long id)
        {
            Period periodById = await GetPeriodById(id) ?? throw new Exception($"Period for ID: {id} not found");
            _dbContext.Periods.Remove(periodById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SoftDeletePeriod(long id)
        {
            Period periodById = await GetPeriodById(id) ?? throw new Exception($"Period for ID: {id} not found");

            periodById.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
