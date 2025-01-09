using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<AppUser?> GetUserByUserNameAsync(string userName)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
    }

    public async Task<AppUser?> GetUserByUserNameWithHome(string userName)
    {
        return await context.Users
            .Include(u => u.Home)
            .SingleOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
    }

    // public async Task<List<AppUser>> GetUsersAsync()
    // {
    //     return await context.Users.ToListAsync();
    // }
}
