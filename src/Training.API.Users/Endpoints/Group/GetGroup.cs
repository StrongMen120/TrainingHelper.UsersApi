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
[Route("api/v{version:apiVersion}/groups/group/{groupId}", Name = "Groups")]
public class GetGroupController : ControllerBase
{
    public GetGroupController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, GetGroupStrategy getGroupStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.GetGroupStrategy = getGroupStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private GetGroupStrategy GetGroupStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpGet(Name = nameof(GetGroup))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetGroup(long groupId)
    {
        try
        {
            var result = await this.GetGroupStrategy.Execute(groupId);
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
