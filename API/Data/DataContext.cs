using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, 
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{
    public DbSet<Home> Homes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<HouseholdDuty> HouseholdDuties { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<Home>()
            .HasMany(h => h.Users)
            .WithOne(u => u.Home)
            .HasForeignKey(u => u.HomeId)
            .IsRequired();

        builder.Entity<Home>()
            .HasMany(h => h.Duties)
            .WithOne(d => d.Home)
            .HasForeignKey(d => d.HomeId)
            .IsRequired();

        builder.Entity<Home>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Home)
            .HasForeignKey(d => d.HomeId)
            .IsRequired();

        builder.Entity<Room>()
            .HasMany(r => r.Duties)
            .WithOne(d => d.Room)
            .HasForeignKey(d => d.RoomId)
            .IsRequired(false);
    }
}
