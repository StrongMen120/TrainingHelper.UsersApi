using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Training.API.Users.Dto;
using Training.API.Users.Services.Abstraction;
using Training.API.Users.Strategy;
using Training.Common.Strategy;

namespace Training.API.Users.Endpoints.Users;

[ApiController]
[Route("api/v{version:apiVersion}/users/user/{userId}", Name = "Users")]
public class GetUserController : ControllerBase
{
    public GetUserController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, GetUserStrategy getUserStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.GetUserStrategy = getUserStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private GetUserStrategy GetUserStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpGet(Name = nameof(GetUser))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetUser(long userId)
    {
        try
        {
            var user = await this.AuthenticationDetailsProvider.GetUserDetails();
            var result = await this.GetUserStrategy.Execute(userId);
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'GetUser'");
            throw;
        }
    }
}
