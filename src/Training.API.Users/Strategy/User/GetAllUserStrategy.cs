using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities;
using Training.API.Users.Dto;
using Training.Common.Strategy;
using ILogger = Serilog.ILogger;

namespace Training.API.Users.Strategy;

public class GetAllUserStrategy : StrategyBase
{
    public GetAllUserStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    public async Task<IEnumerable<UserDto>> Execute()
    {
        var users = await this.TrainingUsersDatabase.Users
            .ToListAsync();
        return this.Mapper.Map<IEnumerable<UserDto>>(users);
    }
}
