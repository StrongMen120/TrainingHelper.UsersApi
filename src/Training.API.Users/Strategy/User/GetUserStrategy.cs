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

public class GetUserStrategy : StrategyBase
{
    public GetUserStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, UsersValidator usersValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.UsersValidator = usersValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly UsersValidator UsersValidator;
    public async Task<UserDto> Execute(long identifier)
    {
        await this.UsersValidator.EnsureValidUsersByIdentifier(identifier);

        var user = await this.TrainingUsersDatabase.Users.FirstOrDefaultAsync(u => u.Identifier == identifier);
        return this.Mapper.Map<UserDto>(user);
    }
}
