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
using Training.API.Users.Strategy;
using Training.Common.Strategy;

namespace Training.API.Users.Endpoints.Users;

[ApiController]
[Route("api/v{version:apiVersion}/groups/groups", Name = "Groups")]
public class GetAllGroupController : ControllerBase
{
    public GetAllGroupController(ILogger logger, IMapper mapper, GetAllGroupStrategy getAllGroupStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.GetAllGroupStrategy = getAllGroupStrategy;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private GetAllGroupStrategy GetAllGroupStrategy { get; }

    [HttpGet(Name = nameof(GetAllGroup))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllGroup()
    {
        try
        {
            var result = await this.GetAllGroupStrategy.Execute();
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'GetAllGroup'");
            throw;
        }
    }
}
