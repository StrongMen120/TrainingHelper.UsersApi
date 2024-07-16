using System.Linq;
using System.Threading.Tasks;
using Training.API.Users.Database;
using Training.Common.Strategy;

namespace Training.API.Users.Strategy;

public sealed class GroupsValidator
{
    public GroupsValidator(TrainingUsersDbContext trainingUsersDatabase)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;

    public async Task EnsureValidGroupByIdentifier(long identifier)
    {
        var group = this.TrainingUsersDatabase.Groups.FirstOrDefault(p => p.Identifier == identifier);

        if (group == null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"Group with identifier:{identifier} not found !");
    }

    public async Task EnsureValidExistGroupByIdentifier(long identifier)
    {
        var group = this.TrainingUsersDatabase.Groups.FirstOrDefault(p => p.Identifier == identifier);

        if (group != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"Group with identifier:{identifier} exist !");
    }

    public async Task EnsureValidExistGroupByName(string nameGroup)
    {
        var group = this.TrainingUsersDatabase.Groups.FirstOrDefault(p => p.Name == nameGroup);

        if (group != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"Group with name:{nameGroup} exist !");
    }

    public async Task EnsureValidExistGroupAssignmentByUserId(long userId, long groupId)
    {
        var groupAssignment = this.TrainingUsersDatabase.GroupsMembers.FirstOrDefault(p => p.UserId == userId && p.GroupId == groupId);

        if (groupAssignment == null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"User with userId:{userId} don't have assignment to group:{groupId} !");
    }
    public async Task EnsureValidGroupAssignmentByUserId(long userId, long groupId)
    {
        var groupAssignment = this.TrainingUsersDatabase.GroupsMembers.FirstOrDefault(p => p.UserId == userId && p.GroupId == groupId);

        if (groupAssignment != null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"User with userId:{userId} have assignment to group:{groupId} !");
    }
}
