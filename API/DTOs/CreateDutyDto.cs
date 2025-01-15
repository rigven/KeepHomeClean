using System;

namespace API.DTOs;

public class CreateDutyDto
{
    public required string Name { get; set; }
    public required int Frequency { get; set; }
    public int? RoomId { get; set; }
}
