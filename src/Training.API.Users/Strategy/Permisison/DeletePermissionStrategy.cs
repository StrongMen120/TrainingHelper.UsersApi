using System;
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

public class DeletePermissionStrategy : StrategyBase
{
    public DeletePermissionStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, RolesValidator rolesValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.RolesValidator = rolesValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly RolesValidator RolesValidator;
    public async Task<PermissionDto> Execute(Guid identifier)
    {
        await this.RolesValidator.EnsureValidPermissionByIdentifier(identifier);

        var result = await this.RemoveEntity(identifier);

        this.Logger.Information($"Permission with id:{identifier} removed !");
        this.Logger.Debug("Permission removed successfully: {@entry}", @result);
        return this.Mapper.Map<PermissionDto>(result);
    }
    private async Task<PermissionEntity> RemoveEntity(Guid identifier)
    {
        var entity = await this.TrainingUsersDatabase.Permission.FirstOrDefaultAsync(p => p.Identifier == identifier);
        var result = this.TrainingUsersDatabase.Remove(entity);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return result.Entity;
    }
}
