using System;
using API.Interfaces;

namespace API.Data;

public class UnitOfWork(DataContext context, IHomeRepository homeRepository, IDutiesRepository dutiesRepository, IRoomRepository roomRepository, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;
    public IHomeRepository HomeRepository => homeRepository;
    public IDutiesRepository DutiesRepository => dutiesRepository;
    public IRoomRepository RoomRepository => roomRepository;
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
