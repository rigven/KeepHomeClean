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
public class RoomController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<RoomDto>> AddRoom(AddRoomDto addRoomDto)
    {
        var userName = User.GetUserName();
        var user = await unitOfWork.UserRepository.GetUserByUserNameWithHome(userName);
        if (user == null) return BadRequest("Cannot find user");

        var home = user.Home;
        var jkfdkjf = home.Rooms.FirstOrDefault(r => r.Name.ToLower() == addRoomDto.Name.ToLower());
        if (home.Rooms.FirstOrDefault(r => r.Name.ToLower() == addRoomDto.Name.ToLower()) != null) return BadRequest("A room with that name already exists");

        Room room = new Room() {
            Name = addRoomDto.Name,
            HomeId = home.Id
        };

        unitOfWork.RoomRepository.AddRoom(room);

        if (await unitOfWork.Complete()) return mapper.Map<RoomDto>(room);

        return BadRequest("Cannot add room");
    }

    [HttpGet]
    public async Task<ActionResult<RoomDto>> GetRooms()
    {
        var userName = User.GetUserName();
        var user = await unitOfWork.UserRepository.GetUserByUserNameAsync(userName);
        if (user == null) return BadRequest("Cannot find user");

        var rooms = await unitOfWork.RoomRepository.GetRoomsForHomeAsync(user.HomeId);

        if (rooms != null) 
            return Ok(mapper.Map<IEnumerable<RoomDto>>(rooms));
        
        return BadRequest("Cannot get rooms");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoom(int id)
    {
        await unitOfWork.RoomRepository.DeleteRoomAsync(id);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Cannot delete room");
    }
}
