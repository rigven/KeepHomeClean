using System;

namespace API.Entities;

public class Home
{
    public int Id { get; set; }
    public ICollection<AppUser> Users { get; set; } = [];
    public ICollection<HouseholdDuty> Duties { get; set; } = [];
    public ICollection<Room> Rooms { get; set; } = [];

}
