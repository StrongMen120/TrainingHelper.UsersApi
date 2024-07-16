using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Training.API.Users.Database.Entities;

namespace Training.API.Users.Dto;

public record GroupMembersDto(
    [property: JsonProperty(Required = Required.Always)]
    long UserId
);
