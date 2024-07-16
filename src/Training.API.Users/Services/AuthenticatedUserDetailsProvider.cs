using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Training.API.Users.Domain;

namespace Training.API.Users.Services.Abstraction;

public class AuthenticationDetailsProvider : IAuthenticationDetailsProvider
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IAuthenticationApiClient authenticationApi;
    private readonly Lazy<ValueTask<string?>> tokenCache;
    private readonly Lazy<ValueTask<UserInfo?>> userInfoCache;

    public AuthenticationDetailsProvider(IHttpContextAccessor contextAccessor, IAuthenticationApiClient authenticationApi)
    {
        this.contextAccessor = contextAccessor;
        this.authenticationApi = authenticationApi;

        tokenCache = new Lazy<ValueTask<string?>>(async () => await (contextAccessor.HttpContext?.GetTokenAsync("access_token") ?? Task.FromResult<string?>(null)));
        userInfoCache = new Lazy<ValueTask<UserInfo?>>(async () => await this.authenticationApi.GetUserInfoAsync(await tokenCache.Value));
    }

    public async Task<UserInfo?> GetCurrentUserInfoAsync() => await this.userInfoCache.Value;

    private Claim? GetCurrentUserClaim(Func<Claim, bool> predicate) => contextAccessor.HttpContext?.User.Claims.FirstOrDefault(predicate);

    public async Task<string?> GetCurrentUserId() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? (await GetCurrentUserInfoAsync())?.UserId;
    public async Task<string?> GetCurrentUserFullName() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.Name)?.Value ?? (await GetCurrentUserInfoAsync())?.FullName;
    public async Task<string?> GetCurrentUserEmail() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.Email)?.Value ?? (await GetCurrentUserInfoAsync())?.Email;

    public async Task<string?> GetAccessToken() => await tokenCache.Value;

    public async Task<UserDetails> GetUserDetails() => new UserDetails(await GetCurrentUserId(), await GetCurrentUserFullName());

    public async Task<string[]> GetAuthority()
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(await tokenCache.Value);
        var auth0Id = jwtSecurityToken.Payload.Aud;
        return auth0Id.ToArray();
    }

}
