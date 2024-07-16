using System;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Users.Dto;

public record UpdateUserCommandDto(
    [property: JsonProperty(Required = Required.Always)]
    long Identifier,

    [property: JsonProperty(Required = Required.Always)] 
    string FirstName,

    [property: JsonProperty(Required = Required.Always)] 
    string SecondName,

    [property: JsonProperty(Required = Required.Always)] 
    string Email,

    [property: JsonProperty(Required = Required.Always)] 
    string Phone,

    [property: JsonProperty(Required = Required.Always)] 
    double Weight,

    [property: JsonProperty(Required = Required.Always)] 
    double Height,

    [property: JsonProperty(Required = Required.Always)] 
    Value.SexTypeDto Sex,
    
    [property: JsonProperty(Required = Required.Always)] 
    LocalDate Birthday
);
