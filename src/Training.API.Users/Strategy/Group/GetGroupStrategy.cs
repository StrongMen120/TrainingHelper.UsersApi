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

public class GetGroupStrategy : StrategyBase
{
    public GetGroupStrategy(ILogger logger, IMapper mapper, TrainingUsersDbContext trainingUsersDatabase, GroupsValidator groupsValidator)
        : base(logger, mapper)
    {
        this.TrainingUsersDatabase = trainingUsersDatabase;
        this.GroupsValidator = groupsValidator;
    }

    private readonly TrainingUsersDbContext TrainingUsersDatabase;
    private readonly GroupsValidator GroupsValidator;
    public async Task<GroupDto> Execute(long groupId)
    {
        await this.GroupsValidator.EnsureValidGroupByIdentifier(groupId);

        var group = await this.TrainingUsersDatabase.Groups
            .Include(p => p.Members)
            .Include(p => p.Trainer)
            .FirstOrDefaultAsync(u => u.Identifier == groupId);
        return this.Mapper.Map<GroupDto>(group);
    }
}
