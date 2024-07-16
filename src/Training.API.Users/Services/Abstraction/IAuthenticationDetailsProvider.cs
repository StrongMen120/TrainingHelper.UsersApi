using System.Threading.Tasks;
using Training.API.Users.Domain;

namespace Training.API.Users.Services.Abstraction;

public interface IAuthenticationDetailsProvider
{
    Task<string?> GetCurrentUserId();
    Task<string?> GetAccessToken();
    Task<UserDetails> GetUserDetails();
}