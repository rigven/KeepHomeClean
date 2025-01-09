using System;

namespace API.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IHomeRepository HomeRepository { get; } 
    public IDutiesRepository DutiesRepository { get; } 
    public IRoomRepository RoomRepository { get; } 
    Task<bool> Complete();
    bool HasChanges();
}
