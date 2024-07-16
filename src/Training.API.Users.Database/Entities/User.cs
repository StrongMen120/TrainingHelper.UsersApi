using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database.Entities;

[Table("Users", Schema = TrainingUsersDatabaseConstants.DefaultSchema)]
public class UserEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public string AuthId { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string SecondName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public double Weight { get; set; }

    [Required]
    public double Height { get; set; }

    [Required]
    public SexType Sex { get; set; }

    [Required]
    public LocalDate Birthday { get; set; }

    public LocalDateTime CreatedAt { get; set; }
    public UserDetails CreatedBy { get; set; }
    public LocalDateTime? ModifiedAt { get; set; }
    public UserDetails? ModifiedBy { get; set; }
    public virtual ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
    public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
}
