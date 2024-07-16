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

public class UpdateRoleStrategy : StrategyBase
{
    public UpdateRoleStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, RolesValidator rolesValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.RolesValidator = rolesValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly RolesValidator RolesValidator;
    public async Task<RoleDto> Execute(RoleDto command, Domain.UserDetails user)
    {
        await this.RolesValidator.EnsureValidRolesByIdentifier(command.Identifier);
        await this.RolesValidator.EnsureValidExistRoleByName(command.Name);

        var result = await this.UpdateEntity(command, user);

        this.Logger.Information($"Roles with id:{command.Identifier} updated !");
        this.Logger.Debug("Roles updated successfully: {@entry}", @result);
        return this.Mapper.Map<RoleDto>(result);
    }
    private async Task<RoleEntity> UpdateEntity(RoleDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Roles.FirstOrDefaultAsync(u => u.Identifier == command.Identifier);
        entry.Name = command.Name;
        entry.Description = command.Description;
        entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
        entry.ModifiedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry;
    }
}
