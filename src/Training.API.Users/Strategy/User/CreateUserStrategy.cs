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

public class CreateUserStrategy : StrategyBase
{
    public CreateUserStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly UsersValidator UsersValidator;

    public async Task<UserDto> Execute(AddUserCommandDto commandDto, Domain.UserDetails user)
    {
        await this.UsersValidator.EnsureValidExistUsersByAuthId(user.Id);

        var entity = await this.BuildEntity(commandDto, user);
        this.Logger.Information($"User with new id:{entity.Identifier} added successfully");
        this.Logger.Debug("User added successfully: {@entity}", @entity);

        return this.Mapper.Map<UserDto>(entity);
    }

    private async Task<UserEntity> BuildEntity(AddUserCommandDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Users.AddAsync(new()
        {
            Identifier = default,
            AuthId = user.Id,
            Birthday = command.Birthday,
            Email = command.Email,
            FirstName = command.FirstName,
            Height = command.Height,
            Phone = command.Phone,
            SecondName = command.SecondName,
            Sex = this.Mapper.Map<Database.Entities.ValueType.SexType>(command.Sex),
            Weight = command.Weight,
            CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
            CreatedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user),
        });

        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry.Entity;
    }
}
