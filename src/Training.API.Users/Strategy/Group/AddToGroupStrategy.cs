using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using Serilog;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities;
using Training.API.Users.Dto;
using Training.Common.Strategy;
using ILogger = Serilog.ILogger;

namespace Training.API.Users.Strategy;

public class AddToGroupStrategy : StrategyBase
{
    public AddToGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.GroupsValidator = groupsValidator;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly GroupsValidator GroupsValidator;
    private readonly UsersValidator UsersValidator;
    public async Task<GroupDto> Execute(AssignedGroupCommandDto command, Domain.UserDetails user)
    {
        await this.UsersValidator.EnsureValidUsersByIdentifier(command.UserId);
        await this.GroupsValidator.EnsureValidGroupByIdentifier(command.GroupId);
        await this.GroupsValidator.EnsureValidGroupAssignmentByUserId(command.UserId, command.GroupId);

        var result = await this.AddEntity(command, user);

        this.Logger.Information($"User with id:{command.UserId} added to group with id:{command.GroupId}!");
        this.Logger.Debug("User assignment update successfully: {@entry}", @result);
        return this.Mapper.Map<GroupDto>(result.Group);
    }
    private async Task<GroupMembersEntity> AddEntity(AssignedGroupCommandDto command, Domain.UserDetails user)
    {
        var result = await this.TrainingUsersDatabase.GroupsMembers.AddAsync(new()
        {
            Identifier = default,
            GroupId = command.GroupId,
            UserId = command.UserId,
            CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
            CreatedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user),
        });
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return result.Entity;
    }
}
