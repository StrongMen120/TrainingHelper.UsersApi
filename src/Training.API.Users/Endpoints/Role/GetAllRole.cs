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
[Route("api/v{version:apiVersion}/roles/roles", Name = "Roles")]
public class GetRoleController : ControllerBase
{
    public GetRoleController(ILogger logger, IMapper mapper, GetAllRoleStrategy getAllRoleStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.GetAllRoleStrategy = getAllRoleStrategy;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private GetAllRoleStrategy GetAllRoleStrategy { get; }

    [HttpGet(Name = nameof(GetAllRoles))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllRoles()
    {
        try
        {
            var result = await this.GetAllRoleStrategy.Execute();
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'GetAllRoles'");
            throw;
        }
    }
}
