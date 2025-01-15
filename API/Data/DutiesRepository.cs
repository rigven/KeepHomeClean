using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DutiesRepository(DataContext dataContext, IMapper mapper) : IDutiesRepository
{
    public void AddDuty(HouseholdDuty duty)
    {
        dataContext.HouseholdDuties.Add(duty);
    }

    public void DeleteDuty(HouseholdDuty duty)
    {
         dataContext.HouseholdDuties.Remove(duty);
    }

    public async Task<IEnumerable<DutyDto>> GetDutiesDtosForHomeAsync(int homeId)
    {
        return await dataContext.HouseholdDuties.Where(d => d.HomeId == homeId).ProjectTo<DutyDto>(mapper.ConfigurationProvider).ToListAsync();
    }
}
