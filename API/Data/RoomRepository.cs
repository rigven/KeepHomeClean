using System;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public class RoomRepository(DataContext dataContext) : IRoomRepository
{
    public void AddRoom(Room room)
    {
        dataContext.Rooms.Add(room);
    }

    public void DeleteRoom(Room room)
    {
        dataContext.Rooms.Remove(room);
    }
}
