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

public class GetAllGroupStrategy : StrategyBase
{
    public GetAllGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    public async Task<IEnumerable<GroupDto>> Execute()
    {
        var groups = await this.TrainingUsersDatabase.Groups.Include(p => p.Trainer).Include(p => p.Members)
            .ToListAsync();
        return this.Mapper.Map<IReadOnlyList<GroupDto>>(groups);
    }
}
