using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database.Entities;

[Table("Roles", Schema = TrainingUsersDatabaseConstants.DefaultSchema)]
public class RoleEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public LocalDateTime CreatedAt { get; set; }
    public UserDetails CreatedBy { get; set; }
    public LocalDateTime? ModifiedAt { get; set; }
    public UserDetails? ModifiedBy { get; set; }

    public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
}
