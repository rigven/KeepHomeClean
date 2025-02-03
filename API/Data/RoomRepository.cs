using System;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class RoomRepository(DataContext dataContext) : IRoomRepository
{
    public void AddRoom(Room room)
    {
        dataContext.Rooms.Add(room);
    }

    public async Task DeleteRoomAsync(int id)
    {
        Room? room = await dataContext.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
        if (room != null)
            dataContext.Rooms.Remove(room);
        else
            throw new Exception("Cannot fond room with the required id");
    }

    public async Task<IEnumerable<Room>> GetRoomsForHomeAsync(int homeId)
    {
        return await dataContext.Rooms.Where(r => r.HomeId == homeId).ToListAsync();
    }
}
