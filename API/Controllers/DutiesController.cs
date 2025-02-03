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
public class DutiesController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
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
            LastTimeDone = DateTime.UtcNow.Date
        };

        unitOfWork.DutiesRepository.AddDuty(duty);

        if (await unitOfWork.Complete()) return Ok(mapper.Map<DutyDto>(duty));

        return BadRequest("Cannot add duty");
    }

    [HttpPatch("markAsDone/{id}")]
    public async Task<ActionResult> MarkAsDone(int id)
    {
        var duty = await unitOfWork.DutiesRepository.GetDutyById(id);
        if (duty == null) return BadRequest("Cannot find duty");

        duty.LastTimeDone = DateTime.UtcNow.Date;

        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Cannot mark duty as done.");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DutyDto>> EditDuty(int id, [FromBody] CreateDutyDto dutyDto)
    {
        var duty = await unitOfWork.DutiesRepository.GetDutyById(id);
        if (duty == null) return BadRequest("Cannot find duty");

        duty.Name = dutyDto.Name;
        duty.Frequency = dutyDto.Frequency;
        duty.RoomId = dutyDto.RoomId;

        if (await unitOfWork.Complete()) return Ok(mapper.Map<DutyDto>(duty));

        return BadRequest("Cannot edit duty.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DutyDto>>> GetDuties()
    {
        var user = await userManager.FindByNameAsync(User.GetUserName());
        if (user == null) return BadRequest("Cannot find user"); 

        return Ok(await unitOfWork.DutiesRepository.GetDutiesDtosForHomeAsync(user.HomeId));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDuty(int id)
    {
        var duty = await unitOfWork.DutiesRepository.GetDutyById(id);
        if (duty == null) return BadRequest("Cannot find duty");

        unitOfWork.DutiesRepository.DeleteDuty(duty);

        if (! await unitOfWork.Complete()) return BadRequest();
        return Ok();
    }
}
