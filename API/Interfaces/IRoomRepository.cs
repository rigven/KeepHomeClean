using System;
using API.Entities;

namespace API.Interfaces;

public interface IRoomRepository
{
    public void AddRoom(Room room);
    public Task DeleteRoomAsync(int id);
    public Task<IEnumerable<Room>> GetRoomsForHomeAsync(int homeId);
}
