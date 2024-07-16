using System.Collections.Generic;
using Newtonsoft.Json;

namespace Training.API.Users.Dto;

public record AssignedGroupCommandDto(
    [property: JsonProperty(Required = Required.Always)]
    long GroupId,

    [property: JsonProperty(Required = Required.Always)]
    long UserId
);
