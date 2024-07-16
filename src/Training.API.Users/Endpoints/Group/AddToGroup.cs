using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
[Route("api/v{version:apiVersion}/groups/add-to-group", Name = "Groups")]
public class AddToGroupController : ControllerBase
{
    public AddToGroupController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, AddToGroupStrategy addToGroupStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.AddToGroupStrategy = addToGroupStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private AddToGroupStrategy AddToGroupStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpPost(Name = nameof(AddToGroup))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> AddToGroup([FromBody, FromQuery] AssignedGroupCommandDto commandDto)
    {
        try
        {
            var user = await this.AuthenticationDetailsProvider.GetUserDetails();
            var result = await this.AddToGroupStrategy.Execute(commandDto, user);
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.Conflict)
        {
            return this.Conflict(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'CreateGroup'");
            throw;
        }
    }
}
