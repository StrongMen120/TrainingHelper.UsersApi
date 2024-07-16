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

public class CreateRoleStrategy : StrategyBase
{
    public CreateRoleStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, RolesValidator rolesValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.RolesValidator = rolesValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly RolesValidator RolesValidator;

    public async Task<RoleDto> Execute(AddRoleCommandDto command, Domain.UserDetails user)
    {
        await this.RolesValidator.EnsureValidRoleByName(command.Name);

        var entity = await this.BuildEntity(command, user);
        this.Logger.Information($"Role with new id:{entity.Identifier} added successfully");
        this.Logger.Debug("Role added successfully: {@entity}", @entity);

        return this.Mapper.Map<RoleDto>(entity);
    }
    private async Task<RoleEntity> BuildEntity(AddRoleCommandDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Roles.AddAsync(new()
        {
            Identifier = default,
            Name = command.Name,
            Description = command.Description,
            CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
            CreatedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user),
        });

        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry.Entity;
    }
}
