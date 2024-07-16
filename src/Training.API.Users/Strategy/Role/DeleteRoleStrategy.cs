using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities;
using Training.API.Users.Dto;
using Training.Common.Strategy;
using ILogger = Serilog.ILogger;

namespace Training.API.Users.Strategy;

public class DeleteRoleStrategy : StrategyBase
{
    public DeleteRoleStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, RolesValidator rolesValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.RolesValidator = rolesValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly RolesValidator RolesValidator;
    public async Task<RoleDto> Execute(long roleId)
    {
        await this.RolesValidator.EnsureValidRolesByIdentifier(roleId);

        var result = await this.RemoveEntity(roleId);

        this.Logger.Information($"Roles with id:{roleId} removed !");
        this.Logger.Debug("Roles removed successfully: {@entry}", @result);
        return this.Mapper.Map<RoleDto>(result);
    }

    private async Task<RoleEntity> RemoveEntity(long roleId)
    {
        var entity = await this.TrainingUsersDatabase.Roles.FirstOrDefaultAsync(p => p.Identifier == roleId);
        var result = this.TrainingUsersDatabase.Remove(entity);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return result.Entity;
    }
}
