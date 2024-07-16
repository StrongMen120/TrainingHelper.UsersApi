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
[Route("api/v{version:apiVersion}/users/users", Name = "Users")]
public class GetAllUserController : ControllerBase
{
    public GetAllUserController(ILogger logger, IMapper mapper, GetAllUserStrategy getAllUserStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.GetAllUserStrategy = getAllUserStrategy;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private GetAllUserStrategy GetAllUserStrategy { get; }

    [HttpGet(Name = nameof(GetAllUser))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllUser()
    {
        try
        {
            var result = await this.GetAllUserStrategy.Execute();
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'GetAllUser'");
            throw;
        }
    }
}
