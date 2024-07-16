using System;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Users.Dto;

public record AddPermissionCommandDto(
    [property: JsonProperty(Required = Required.Always)]
    long UserId,

    [property: JsonProperty(Required = Required.Always)]
    long RoleId
);
