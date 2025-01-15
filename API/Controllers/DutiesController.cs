using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class DutiesController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpPost("add-duty")]
    public async Task<ActionResult> AddDuty(CreateDutyDto dutyDto) 
    {
        var user = await userManager.FindByNameAsync(User.GetUserName());
        if (user == null) return BadRequest("Cannot find user"); 
        HouseholdDuty duty = new HouseholdDuty()
        {
            Name = dutyDto.Name,
            Frequency = dutyDto.Frequency,
            HomeId = user.HomeId,
            RoomId = dutyDto.RoomId,
            LastTimeDone = DateTime.UtcNow
        };

        unitOfWork.DutiesRepository.AddDuty(duty);

        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Cannot add duty");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DutyDto>>> GetDuties()
    {
        var user = await userManager.FindByNameAsync(User.GetUserName());
        if (user == null) return BadRequest("Cannot find user"); 

        return Ok(await unitOfWork.DutiesRepository.GetDutiesDtosForHomeAsync(user.HomeId));
    }
}
