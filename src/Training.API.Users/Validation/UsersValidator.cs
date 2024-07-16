using System.Linq;
using System.Threading.Tasks;
using Training.API.Users.Database;
using Training.Common.Strategy;

namespace Training.API.Users.Strategy;

public sealed class UsersValidator
{
    public UsersValidator(TrainingUsersDbContext trainingUsersDatabase)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;

    public async Task EnsureValidUsersByIdentifier(long identifier)
    {
        var user = this.TrainingUsersDatabase.Users.FirstOrDefault(p => p.Identifier == identifier);

        if (user == null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"User with identifier:{identifier} not found !");
    }

    public async Task EnsureValidExistUsersByIdentifier(long identifier)
    {
        var user = this.TrainingUsersDatabase.Users.FirstOrDefault(p => p.Identifier == identifier);

        if (user != null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"User with identifier:{identifier} exist !");
    }

    public async Task EnsureValidExistUsersByAuthId(string authId)
    {
        var user = this.TrainingUsersDatabase.Users.FirstOrDefault(p => p.AuthId == authId);

        if (user != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"User with authId:{authId} exist !");
    }

    public async Task EnsureValidUsersByAuthId(string authId)
    {
        var user = this.TrainingUsersDatabase.Users.FirstOrDefault(p => p.AuthId == authId);

        if (user == null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"User with authId:{authId} not found !");
    }
}
