using Application.RoleManagement.Commands.CreateRole;
using Application.RoleManagement.Commands.DeleteRole;
using Application.RoleManagement.Commands.UpdateRole;
using Application.RoleManagement.Queries.GetRole;
using Application.RoleManagement.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers.v1
{
    [Authorize(Roles = "Administrator")]
    [Route("api/v1/role")]
    public class RoleController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRolesResponse>> GetAllasync([FromQuery] int? page, int? pageSize)
        {
            var request = new GetRolesRequest
            {
                Page = page,
                PageSize = pageSize,
            };

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRoleResponse>> GetAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetRoleRequest { RoleId = id }));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRole command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateRole command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteRole
            {
                RoleId = id,
            };

            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}