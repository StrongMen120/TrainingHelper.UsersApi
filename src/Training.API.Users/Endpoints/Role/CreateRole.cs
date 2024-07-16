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
[Route("api/v{version:apiVersion}/roles/role", Name = "Roles")]
public class CreateRoleController : ControllerBase
{
    public CreateRoleController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, CreateRoleStrategy createRoleStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.CreateRoleStrategy = createRoleStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private CreateRoleStrategy CreateRoleStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpPost(Name = nameof(CreateRole))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> CreateRole([Required, FromBody] AddRoleCommandDto command)
    {
        try
        {
            var user = await this.AuthenticationDetailsProvider.GetUserDetails();
            var result = await this.CreateRoleStrategy.Execute(command, user);
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
            this.Logger.Error(e, "Error while executing 'CreateRole'");
            throw;
        }
    }
}
