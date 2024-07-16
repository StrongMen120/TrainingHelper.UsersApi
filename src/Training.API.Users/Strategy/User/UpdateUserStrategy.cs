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

public class UpdateUserStrategy : StrategyBase
{
    public UpdateUserStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly UsersValidator UsersValidator;
    public async Task<UserDto> Execute(UpdateUserCommandDto commandDto, Domain.UserDetails user)
    {
        await this.UsersValidator.EnsureValidUsersByIdentifier(commandDto.Identifier);

        var result = await this.UpdateEntity(commandDto, user);

        this.Logger.Information($"User with id:{commandDto.Identifier} updated!");
        this.Logger.Debug("User update successfully: {@entry}", @result);
        return this.Mapper.Map<UserDto>(result);
    }

    private async Task<UserEntity> UpdateEntity(UpdateUserCommandDto command, Domain.UserDetails user)
    {
        var entry = await this.TrainingUsersDatabase.Users.FirstOrDefaultAsync(user => user.Identifier == command.Identifier);
        entry.Birthday = command.Birthday;
        entry.Email = command.Email;
        entry.FirstName = command.FirstName;
        entry.Height = command.Height;
        entry.Phone = command.Phone;
        entry.SecondName = command.SecondName;
        entry.Sex = this.Mapper.Map<Database.Entities.ValueType.SexType>(command.Sex);
        entry.Weight = command.Weight;
        entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
        entry.ModifiedBy = this.Mapper.Map<Database.Entities.ValueType.UserDetails>(user);
        await this.TrainingUsersDatabase.SaveChangesAsync();

        return entry;
    }
}
