using System;
using System.Linq;
using System.Threading.Tasks;
using Training.API.Users.Database;
using Training.Common.Strategy;

namespace Training.API.Users.Strategy;

public sealed class RolesValidator
{
    public RolesValidator(TrainingUsersDbContext trainingUsersDatabase)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;

    public async Task EnsureValidRolesByIdentifier(long identifier)
    {
        var roles = this.TrainingUsersDatabase.Roles.FirstOrDefault(p => p.Identifier == identifier);

        if (roles == null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"Role with identifier:{identifier} not found !");
    }

    public async Task EnsureValidExistRolesByIdentifier(long identifier)
    {
        var roles = this.TrainingUsersDatabase.Roles.FirstOrDefault(p => p.Identifier == identifier);

        if (roles != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"Role with identifier:{identifier} exist !");
    }

    public async Task EnsureValidExistRoleByName(string roleName)
    {
        var roles = this.TrainingUsersDatabase.Roles.FirstOrDefault(p => p.Name == roleName);

        if (roles != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"Role with name:{roleName} exist !");
    }

    public async Task EnsureValidRoleByName(string roleName)
    {
        var roles = this.TrainingUsersDatabase.Roles.FirstOrDefault(p => p.Name == roleName);

        if (roles != null)
            throw new StrategyException(System.Net.HttpStatusCode.NotFound, $"Role with name:{roleName} not found !");
    }

    public async Task EnsureValidPermissionByUserId(long userId, long roleId)
    {
        var permission = this.TrainingUsersDatabase.Permission.FirstOrDefault(p => p.UserId == userId && p.RoleId == roleId);

        if (permission != null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"User with userId:{userId} have permission to role:{roleId} !");
    }

    public async Task EnsureValidPermissionByIdentifier(Guid identifier)
    {
        var permission = this.TrainingUsersDatabase.Permission.FirstOrDefault(p => p.Identifier == identifier);

        if (permission == null)
            throw new StrategyException(System.Net.HttpStatusCode.Conflict, $"Permission with permissionId:{identifier} not found !");
    }
}
