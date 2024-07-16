using Microsoft.EntityFrameworkCore;

namespace Training.API.Users.Database.Entities.ValueType;

[Owned]
public class UserDetails
{
    public string Id { get; set; }
    public string FullName { get; set; }
}

