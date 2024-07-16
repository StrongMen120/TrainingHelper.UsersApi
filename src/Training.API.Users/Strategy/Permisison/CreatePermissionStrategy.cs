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

public class CreatePermissionStrategy : StrategyBase
{
    public CreatePermissionStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, RolesValidator rolesValidator, UsersValidator usersValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.RolesValidator = rolesValidator;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly RolesValidator RolesValidator;
    private readonly UsersValidator UsersValidator;
    public async Task<PermissionDto> Execute(AddPermissionCommandDto command, Domain.UserDetails user)
    {
        await this.RolesValidator.EnsureValidRolesByIdentifier(command.RoleId);
        await this.UsersValidator.EnsureValidUsersByIdentifier(command.UserId);
        await this.RolesValidator.EnsureValidPermissionByUserId(command.UserId, command.RoleId);

        var result = await this.BuildEntity(command, user);

        this.Logger.Information($"Permission with id:{result.Identifier} added !");
        this.Logger.Debug("Permission added successfully: {@entry}", @result);
        return this.Mapper.Map<PermissionDto>(result);
    }
    private async Task<PermissionEntity> BuildEntity(AddPermissionCommandDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Permission.AddAsync(new()
        {
            Identifier = default,
            RoleId = command.RoleId,
            UserId = command.UserId,
            CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
            CreatedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user),
        });

        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry.Entity;
    }
}
