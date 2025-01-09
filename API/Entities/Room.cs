using System;

namespace API.Entities;

public class Room
{
    public int Id { get; set;}
    public required string Name { get; set;}
    public ICollection<HouseholdDuty> Duties { get; set; } = [];
    public required int HomeId { get; set; }
    public Home? Home { get; set; }
}
