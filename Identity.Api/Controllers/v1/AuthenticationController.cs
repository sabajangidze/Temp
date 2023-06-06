﻿using Application.IdentityManagement.Commands.CreateAuthenticationToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.V1
{

    [Route("api/v1/authentication")]
    public class AuthenticationController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] CreateAuthenticationTokenRequest request)
        {
            var result = await Mediator.Send(request);

            return !result.Success ? StatusCode(401) : StatusCode(200, result);
        }
    }
}