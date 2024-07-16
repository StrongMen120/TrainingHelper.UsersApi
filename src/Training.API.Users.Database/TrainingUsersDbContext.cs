using Microsoft.EntityFrameworkCore;
using Npgsql;
using Training.API.Users.Database.Entities;
using Training.API.Users.Database.Entities.ValueType;

namespace Training.API.Users.Database;

public class TrainingUsersDbContext : DbContext
{
    protected TrainingUsersDbContext()
    { }

    public TrainingUsersDbContext(DbContextOptions<TrainingUsersDbContext> options)
        : base(options)
    { }
    public DbSet<UserEntity> Users { get; init; } = null!;
    public DbSet<GroupEntity> Groups { get; init; } = null!;
    public DbSet<GroupMembersEntity> GroupsMembers { get; init; } = null!;
    public DbSet<RoleEntity> Roles { get; init; } = null!;
    public DbSet<PermissionEntity> Permission { get; init; } = null!;
    private static void RegisterEnums()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<SexType>($"{TrainingUsersDatabaseConstants.DefaultSchema}.{nameof(SexType)}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        RegisterEnums(); // Register all enums used in entities
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresEnum<SexType>(TrainingUsersDatabaseConstants.DefaultSchema, nameof(SexType));
        builder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
        });
        builder.Entity<PermissionEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.User).WithMany(e => e.Permissions).HasForeignKey(e => e.UserId);
            entity.HasOne(a => a.Role).WithMany(e => e.Permissions).HasForeignKey(e => e.RoleId);
        });
        builder.Entity<GroupEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.Trainer).WithMany(e => e.Groups).HasForeignKey(e => e.TrainerId);
        });
        builder.Entity<GroupMembersEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.Group).WithMany(e => e.Members).HasForeignKey(e => e.GroupId);
        });
    }
}
