using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.UserName)) return BadRequest("UserName is taken");

        var user = mapper.Map<AppUser>(registerDto);
        user.UserName = registerDto.UserName.ToLower();

        // Create home for user
        Home home = new Home();
        home.Users.Add(user);
        //user.HomeId = home.Id;
        user.Home = home;

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return new UserDto
        {
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Token = await tokenService.CreateToken(user),
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedUserName == loginDto.UserName.ToUpper());

        if (user == null || user.UserName == null || !await userManager.CheckPasswordAsync(user, loginDto.Password)) 
            return Unauthorized("Invalid credentials");

        return new UserDto
        {
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Token = await tokenService.CreateToken(user),
        };
    }

    private async Task<bool> UserExists(string userName)
    {
        return await userManager.Users.AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
    }
}
