using System;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public class DutiesRepository(DataContext dataContext) : IDutiesRepository
{
    public void AddDuty(HouseholdDuty duty)
    {
        dataContext.HouseholdDuties.Add(duty);
    }

    public void DeleteDuty(HouseholdDuty duty)
    {
         dataContext.HouseholdDuties.Remove(duty);
    }
}
