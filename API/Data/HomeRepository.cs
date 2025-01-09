using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class HomeRepository(DataContext dataContext) : IHomeRepository
{
    public void AddHome(Home home)
    {
        dataContext.Homes.Add(home);
    }
}
