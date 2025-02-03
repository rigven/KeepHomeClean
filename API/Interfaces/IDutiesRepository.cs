using System;
using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IDutiesRepository
{
    Task<HouseholdDuty?> GetDutyById(int dutyId);
    void AddDuty(HouseholdDuty duty);
    void DeleteDuty(HouseholdDuty duty);
    Task<IEnumerable<DutyDto>> GetDutiesDtosForHomeAsync(int homeId);
    
}
