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
[Route("api/v{version:apiVersion}/permissions/permission/{permissionId}", Name = "Permissions")]
public class DeletePermissionController : ControllerBase
{
    public DeletePermissionController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, DeletePermissionStrategy deletePermissionStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.DeletePermissionStrategy = deletePermissionStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private DeletePermissionStrategy DeletePermissionStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpDelete(Name = nameof(DeletePermission))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeletePermission(Guid permissionId)
    {
        try
        {
            var result = await this.DeletePermissionStrategy.Execute(permissionId);
            return this.Ok(result);
        }
        catch (StrategyException e) when (e.Status == HttpStatusCode.NotFound)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            this.Logger.Error(e, "Error while executing 'GetAllPermission'");
            throw;
        }
    }
}
