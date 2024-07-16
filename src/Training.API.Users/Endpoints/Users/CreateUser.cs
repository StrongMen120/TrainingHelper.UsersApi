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
[Route("api/v{version:apiVersion}/users/user", Name = "Users")]
public class CreateUserController : ControllerBase
{
    public CreateUserController(ILogger logger, IMapper mapper, IAuthenticationDetailsProvider authenticationDetailsProvider, CreateUserStrategy createUserStrategy)
    {
        this.Logger = logger;
        this.Mapper = mapper;
        this.CreateUserStrategy = createUserStrategy;
        this.AuthenticationDetailsProvider = authenticationDetailsProvider;
    }

    private ILogger Logger { get; }
    private IMapper Mapper { get; }
    private CreateUserStrategy CreateUserStrategy { get; }
    protected IAuthenticationDetailsProvider AuthenticationDetailsProvider { get; }

    [HttpPost(Name = nameof(CreateUser))]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async ValueTask<IActionResult> CreateUser([Required, FromBody] AddUserCommandDto command)
    {
        try
        {
            var user = await this.AuthenticationDetailsProvider.GetUserDetails();
            var result = await this.CreateUserStrategy.Execute(command, user);
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
            this.Logger.Error(e, "Error while executing 'CreateUser'");
            throw;
        }
    }
}
