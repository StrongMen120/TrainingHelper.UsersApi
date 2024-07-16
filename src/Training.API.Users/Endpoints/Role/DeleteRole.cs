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
[Route("api/v{version:apiVersion}/roles/role/{roleId}", Name = "Roles")]
public class DeleteRoleController : ControllerBase
{
    public DeleteRoleController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, DeleteRoleStrategy deleteRoleStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.DeleteRoleStrategy = deleteRoleStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private DeleteRoleStrategy DeleteRoleStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpDelete(Name = nameof(DeleteRole))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteRole(long roleId)
    {
        try
        {
            var result = await this.DeleteRoleStrategy.Execute(roleId);
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'DeleteRole'");
            throw;
        }
    }
}
