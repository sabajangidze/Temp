using Application.EventManagement.Commands.CreateEvent;
using Application.EventManagement.Commands.DeleteEvent;
using Application.EventManagement.Commands.UpdateEvent;
using Application.EventManagement.Queries.GetEvent;
using Application.EventManagement.Queries.GetEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Admin.Api.Controllers.v1
{
    [Route("api/v1/event")]
    public class EventController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetEventsResponse>> GetAllasync([FromQuery] int? page, int? pageSize)
        {
            var request = new GetEventsRequest
            {
                Page = page,
                PageSize = pageSize,
            };

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetEventResponse>> GetAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetEventRequest { EventId = id }));
        }

        [Authorize(Policy = RoleClaims.Event_Create)]
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEvent command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [Authorize(Policy = RoleClaims.Event_Update)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateEvent command)
        {
            await Mediator.Send(command);

            return Ok();
        }
        
        [Authorize(Policy = RoleClaims.Event_Update)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BookEvent([FromBody] BookEvent command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = RoleClaims.Event_Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteEvent
            {
                EventId = id,
            };

            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}