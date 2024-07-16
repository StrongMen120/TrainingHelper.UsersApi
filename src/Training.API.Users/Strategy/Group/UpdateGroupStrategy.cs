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

public class UpdateGroupStrategy : StrategyBase
{
    public UpdateGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.UsersValidator = usersValidator;
        this.GroupsValidator = groupsValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly UsersValidator UsersValidator;
    private readonly GroupsValidator GroupsValidator;
    public async Task<GroupDto> Execute(UpdateGroupCommandDto command, Domain.UserDetails user)
    {
        await this.GroupsValidator.EnsureValidGroupByIdentifier(command.Identifier);
        await this.GroupsValidator.EnsureValidExistGroupByName(command.Name);
        await this.UsersValidator.EnsureValidUsersByIdentifier(command.TrainerId);

        var result = await this.UpdateEntity(command, user);

        this.Logger.Information($"Group with id:{command.Identifier} updated!");
        this.Logger.Debug("Group update successfully: {@entry}", @result);
        return this.Mapper.Map<GroupDto>(result);
    }
    private async Task<GroupEntity> UpdateEntity(UpdateGroupCommandDto command, Domain.UserDetails user)
    {

        var entry = await this.TrainingUsersDatabase.Groups.FirstOrDefaultAsync(user => user.Identifier == command.Identifier);
        entry.Name = command.Name;
        entry.TrainerId = command.TrainerId;
        entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
        entry.ModifiedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user);

        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry;
    }
}
