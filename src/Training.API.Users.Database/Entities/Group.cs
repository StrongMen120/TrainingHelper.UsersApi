using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database.Entities;

[Table("Groups", Schema = TrainingUsersDatabaseConstants.DefaultSchema)]
public class GroupEntity
{
    [Key]
    public long Identifier { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public long TrainerId { get; set; }

    public LocalDateTime CreatedAt { get; set; }
    public UserDetails CreatedBy { get; set; }
    public LocalDateTime? ModifiedAt { get; set; }
    public UserDetails? ModifiedBy { get; set; }
    public virtual UserEntity Trainer { get; set; } = null!;
    public virtual ICollection<GroupMembersEntity> Members { get; set; } = new List<GroupMembersEntity>();
}
