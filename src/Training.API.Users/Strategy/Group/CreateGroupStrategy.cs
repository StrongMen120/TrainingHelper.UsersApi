using System.Threading.Tasks;
using MapsterMapper;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities;
using Training.API.Users.Dto;
using Training.Common.Strategy;
using ILogger = Serilog.ILogger;

namespace Training.API.Users.Strategy;

public class CreateGroupStrategy : StrategyBase
{
    public CreateGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.UsersValidator = usersValidator;
        this.GroupsValidator = groupsValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly UsersValidator UsersValidator;
    private readonly GroupsValidator GroupsValidator;

    public async Task<GroupDto> Execute(AddGroupCommandDto command, Domain.UserDetails user)
    {
        await this.UsersValidator.EnsureValidUsersByIdentifier(command.TrainerId);

        var entity = await this.BuildEntity(command, user);
        this.Logger.Information($"Group with new id:{entity.Identifier} added successfully");
        this.Logger.Debug("Group added successfully: {@entity}", @entity);

        return this.Mapper.Map<GroupDto>(entity);
    }

    private async Task<GroupEntity> BuildEntity(AddGroupCommandDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Groups.AddAsync(new()
        {
            Identifier = default,
            TrainerId = command.TrainerId,
            Name = command.Name,
            CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
            CreatedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user),
        });

        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry.Entity;
    }
}
