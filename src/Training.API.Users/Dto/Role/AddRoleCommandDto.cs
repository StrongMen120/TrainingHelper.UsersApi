using System;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Users.Dto;

public record AddRoleCommandDto(
    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    string Description
);
