using Application.Features.Models.Queries.GetListUserProfileByDynamicQuery;
using Application.Features.Technologies.Commands.CreatedUserProfile;
using Application.Features.Technologies.Commands.DeleteUserProfileCommand;
using Application.Features.Technologies.Commands.UpdatedUserProfileCommand;
using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Kodlama.io.Devs.Application.Features.UserProfiles.Querıes.GetByIdUserProfile;
using Kodlama.io.Devs.Application.Features.UserProfiles.Queries.GetByListUserProfile;
using Kodlama.io.Devs.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreatedUserProfileCommand createUserProfileCommand)
        {
            CreatedUserProfileDto createdUserProfileDto = await Mediator.Send(createUserProfileCommand);
            return Created("", createdUserProfileDto);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserProfileCommand deleteUserProfileCommand)
        {
            DeletedUserProfileDto deletedUserProfileDto = await Mediator.Send(deleteUserProfileCommand);
            return Ok(deletedUserProfileDto);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatedUserProfileCommand updateUserProfileCommand)
        {
            UpdatedUserProfileDto updatedUserProfileDto = await Mediator.Send(updateUserProfileCommand);
            return Ok(updatedUserProfileDto);
        }
        [HttpGet("{Id}")]

        public async Task<IActionResult> GetById([FromRoute] GetByIdUserProfileQuery getByIdUserProfileQuery)
        {
            UserProfileGetByIdDto UserProfileGetByIdDto = await Mediator.Send(getByIdUserProfileQuery);

            return Ok(UserProfileGetByIdDto);
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserProfileQuery getListUserProfileQuery = new() { PageRequest = pageRequest };
            UserProfileListModel result = await Mediator.Send(getListUserProfileQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListUserProfileByDynamicQuery getListUserProfileByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            UserProfileListModel result = await Mediator.Send(getListUserProfileByDynamicQuery);
            return Ok(result);
        }
    }
}
