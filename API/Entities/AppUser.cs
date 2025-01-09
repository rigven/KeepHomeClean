using System;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{
    public required string KnownAs { get; set; }
    public required int HomeId { get; set; }
    public required Home Home { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; } = [];

}
