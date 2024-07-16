using System;
using Newtonsoft.Json;
using NodaTime;

namespace Training.API.Users.Domain;

public record UserDetails(
string Id,
string FullName);