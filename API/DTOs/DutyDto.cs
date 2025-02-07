using System;

namespace API.DTOs;

public class DutyDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Frequency { get; set; }
    public DateTime LastTimeDone { get; set; }
    public int? RoomId { get; set; }
}
