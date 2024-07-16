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

public class RemoveFromGroupStrategy : StrategyBase
{
    public RemoveFromGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.GroupsValidator = groupsValidator;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly GroupsValidator GroupsValidator;
    private readonly UsersValidator UsersValidator;

    public async Task<GroupDto> Execute(AssignedGroupCommandDto command)
    {
        await this.UsersValidator.EnsureValidUsersByIdentifier(command.UserId);
        await this.GroupsValidator.EnsureValidGroupByIdentifier(command.GroupId);
        await this.GroupsValidator.EnsureValidExistGroupAssignmentByUserId(command.UserId, command.GroupId);

        var result = await this.RemoveEntity(command);

        this.Logger.Information($"User with id:{command.UserId} removed from group with id:{command.GroupId}!");
        this.Logger.Debug("User assignment update successfully: {@entry}", @result);
        return this.Mapper.Map<GroupDto>(result.Group);
    }

    private async Task<GroupMembersEntity> RemoveEntity(AssignedGroupCommandDto command)
    {
        var entity = await this.TrainingUsersDatabase.GroupsMembers.FirstOrDefaultAsync(p => p.GroupId == command.GroupId && p.UserId == command.UserId);
        var result = this.TrainingUsersDatabase.Remove(entity);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return result.Entity;
    }
}
