﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Training.API.Users.Database;
using Training.API.Users.Database.Entities.ValueType;

#nullable disable

namespace Training.API.Users.Database.Migrations
{
    [DbContext(typeof(TrainingUsersDbContext))]
    [Migration("20230221130250_InitialCreation")]
    partial class InitialCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "users", "SexType", new[] { "mean", "woman" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Training.API.Users.Database.Entities.GroupEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TrainerId")
                        .HasColumnType("bigint");

                    b.HasKey("Identifier");

                    b.HasIndex("TrainerId");

                    b.ToTable("Groups", "users");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.GroupMembersEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Identifier");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMembers", "users");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.PermissionEntity", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Identifier");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions", "users");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.RoleEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Identifier");

                    b.ToTable("Roles", "users");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.UserEntity", b =>
                {
                    b.Property<long>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Identifier"));

                    b.Property<string>("AuthId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<LocalDate>("Birthday")
                        .HasColumnType("date");

                    b.Property<LocalDateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<LocalDateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<SexType>("Sex")
                        .HasColumnType("users.\"SexType\"");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Identifier");

                    b.ToTable("Users", "users");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.GroupEntity", b =>
                {
                    b.HasOne("Training.API.Users.Database.Entities.UserEntity", "Trainer")
                        .WithMany("Groups")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.GroupMembersEntity", b =>
                {
                    b.HasOne("Training.API.Users.Database.Entities.GroupEntity", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Training.API.Users.Database.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.PermissionEntity", b =>
                {
                    b.HasOne("Training.API.Users.Database.Entities.RoleEntity", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Training.API.Users.Database.Entities.UserEntity", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.UserEntity", b =>
                {
                    b.OwnsOne("Training.API.Users.Database.Entities.ValueType.UserDetails", "CreatedBy", b1 =>
                        {
                            b1.Property<long>("UserEntityIdentifier")
                                .HasColumnType("bigint");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserEntityIdentifier");

                            b1.ToTable("Users", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityIdentifier");
                        });

                    b.OwnsOne("Training.API.Users.Database.Entities.ValueType.UserDetails", "ModifiedBy", b1 =>
                        {
                            b1.Property<long>("UserEntityIdentifier")
                                .HasColumnType("bigint");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserEntityIdentifier");

                            b1.ToTable("Users", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityIdentifier");
                        });

                    b.Navigation("CreatedBy")
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.GroupEntity", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.RoleEntity", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Training.API.Users.Database.Entities.UserEntity", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
