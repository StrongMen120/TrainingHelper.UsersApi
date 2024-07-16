using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database.Entities;

[Table("GroupMembers", Schema = TrainingUsersDatabaseConstants.DefaultSchema)]
public class GroupMembersEntity
{
    [Key]
    public Guid Identifier { get; set; }

    [Required]
    public long GroupId { get; set; }

    [Required]
    public long UserId { get; set; }

    public LocalDateTime CreatedAt { get; set; }
    public UserDetails CreatedBy { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual GroupEntity Group { get; set; } = null!;
}
