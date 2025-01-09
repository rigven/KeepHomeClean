using System;
using API.Entities;

namespace API.Interfaces;

public interface IRoomRepository
{
    public void AddRoom(Room room);
    public void DeleteRoom(Room room);
}
