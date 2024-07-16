using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities;
using Training.API.Users.Dto;
using Training.Common.Strategy;
using ILogger = Serilog.ILogger;

namespace Training.API.Users.Strategy;

public class DeleteGroupStrategy : StrategyBase
{
    public DeleteGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.GroupsValidator = groupsValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly GroupsValidator GroupsValidator;

    public async Task<GroupDto> Execute(long groupId)
    {
        await this.GroupsValidator.EnsureValidGroupByIdentifier(groupId);

        var result = await this.RemoveEntity(groupId);

        this.Logger.Information($"Group with id:{groupId} removed !");
        this.Logger.Debug("Group removed successfully: {@entry}", @result);
        return this.Mapper.Map<GroupDto>(result);
    }

    private async Task<GroupEntity> RemoveEntity(long groupId)
    {
        var entity = await this.TrainingUsersDatabase.Groups.FirstOrDefaultAsync(p => p.Identifier == groupId);
        var result = this.TrainingUsersDatabase.Remove(entity);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return result.Entity;
    }
}
