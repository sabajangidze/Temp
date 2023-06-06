using Application.RoleClaimManagement.Commands.CreateRoleClaim;
using Application.RoleClaimManagement.Commands.DeleteRoleClaim;
using Application.RoleClaimManagement.Queries.GetRoleClaims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers.v1
{
    [Authorize(Roles = "Administrator")]
    [Route("api/v1/roleclaim")]
    public class RoleClaimController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRoleClaimsResponse>> GetAllasync([FromQuery] int? page, int? pageSize, Guid? RoleId)
        {
            var request = new GetRoleClaimsRequest
            {
                Page = page,
                PageSize = pageSize,
                RoleId = RoleId,
            };

            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleClaim command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteRoleClaim
            {
                RoleClaimId = id,
            };

            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}