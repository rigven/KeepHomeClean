using System;

namespace API.Entities;

public class HouseholdDuty
{
    public int Id { get; set; }
    public required string Name { get; set; }
    /// <summary>
    /// Number of days between duty repetition
    /// </summary>
    public required int Frequency { get; set; }
    public DateTime LastTimeDone { get; set; }
    public int? RoomId { get; set; }
    public Room? Room { get; set; }
    public required int HomeId { get; set; }
    public Home? Home { get; set; }
}
