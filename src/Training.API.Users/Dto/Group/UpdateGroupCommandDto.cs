using System.Collections.Generic;
using Newtonsoft.Json;

namespace Training.API.Users.Dto;

public record UpdateGroupCommandDto(
    [property: JsonProperty(Required = Required.Always)]
    long Identifier,

    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    long TrainerId
);
