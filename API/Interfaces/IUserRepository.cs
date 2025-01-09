using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetUserByUserNameAsync(string userName);
    Task<AppUser?> GetUserByUserNameWithHome(string userName);
    // Task<List<AppUser>> GetUsersAsync();
}
