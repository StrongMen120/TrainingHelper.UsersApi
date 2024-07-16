using System;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Users.Dto;

public record PermissionDto(
    [property: JsonProperty(Required = Required.Always)]
    Guid Identifier,

    [property: JsonProperty(Required = Required.Always)]
    UserDto User,

    [property: JsonProperty(Required = Required.Always)]
    RoleDto Role
);
