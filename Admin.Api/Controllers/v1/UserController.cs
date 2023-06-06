using Application.UserManagement.Commands.ChangeRole;
using Application.UserManagement.Commands.CreateUser;
using Application.UserManagement.Commands.DeleteUser;
using Application.UserManagement.Commands.UpdateUser;
using Application.UserManagement.Queries.GetUser;
using Application.UserManagement.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers.v1
{
    [Authorize(Roles = "Administrator")]
    [Route("api/v1/user")]
    public class UserController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUsersResponse>> GetAllasync([FromQuery] int? page, int? pageSize)
        {
            var request = new GetUsersRequest
            {
                Page = page,
                PageSize = pageSize,
            };

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserResponse>> GetAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserRequest { UserId = id }));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUser command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateUser command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("change-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ChangeRoleAsync(ChangeRole command)
        {
            _ = await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteUser
            {
                UserId = id,
            };

            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}