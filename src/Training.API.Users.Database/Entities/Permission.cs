using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database.Entities;

[Table("Permissions", Schema = TrainingUsersDatabaseConstants.DefaultSchema)]
public class PermissionEntity
{
    [Key]
    public Guid Identifier { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public long RoleId { get; set; }

    public LocalDateTime CreatedAt { get; set; }
    public UserDetails CreatedBy { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual RoleEntity Role { get; set; } = null!;
}
