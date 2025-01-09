using System;
using API.Entities;

namespace API.Interfaces;

public interface IDutiesRepository
{
    public void AddDuty(HouseholdDuty duty);
    public void DeleteDuty(HouseholdDuty duty);
    
}
