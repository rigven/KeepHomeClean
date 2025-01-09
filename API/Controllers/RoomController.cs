using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RoomController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<RoomDto>> AddRoom(AddRoomDto addRoomDto)
    {
        var userName = User.GetUserName();
        var user = await unitOfWork.UserRepository.GetUserByUserNameWithHome(userName);
        if (user == null) return BadRequest("Cannot find user");

        var home = user.Home;
        if (home.Rooms.FirstOrDefault(r => r.Name == addRoomDto.Name) != null) return BadRequest("A room with that name already exists");

        Room room = new Room() {
            Name = addRoomDto.Name,
            HomeId = home.Id
        };

        unitOfWork.RoomRepository.AddRoom(room);

        if (await unitOfWork.Complete()) return mapper.Map<RoomDto>(room);

        return BadRequest("Cannot add room");
    }
}
