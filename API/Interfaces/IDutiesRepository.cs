using System;
using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IDutiesRepository
{
    public void AddDuty(HouseholdDuty duty);
    public void DeleteDuty(HouseholdDuty duty);
    Task<IEnumerable<DutyDto>> GetDutiesDtosForHomeAsync(int homeId);
    
}
