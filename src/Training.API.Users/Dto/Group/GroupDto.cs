using System.Collections.Generic;
using Newtonsoft.Json;

namespace Training.API.Users.Dto;

public record GroupDto(
    [property: JsonProperty(Required = Required.Always)]
    long Identifier,

    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    UserDto Trainer,

    [property: JsonProperty(Required = Required.Always)]
    IEnumerable<GroupMembersDto> Members
);
