using System;
using Mapster;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Users.Database.Entities;
using Training.API.Users.Database.Entities.ValueType;
using Training.API.Users.Dto;
using Training.API.Users.Dto.Value;

namespace Training.API.Users.Mappings;

internal class MappingRegistry : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserEntity, UserDto>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.FirstName, s => s.FirstName)
            .Map(d => d.SecondName, s => s.SecondName)
            .Map(d => d.Email, s => s.Email)
            .Map(d => d.Phone, s => s.Phone)
            .Map(d => d.Weight, s => s.Weight)
            .Map(d => d.Height, s => s.Height)
            .Map(d => d.Sex, s => s.Sex)
            .Map(d => d.Birthday, s => s.Birthday)
            .ShallowCopyForSameType(false);

        config.NewConfig<RoleEntity, RoleDto>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Description, s => s.Description)
            .ShallowCopyForSameType(false);

        config.NewConfig<PermissionEntity, PermissionDto>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.User, s => s.User)
            .Map(d => d.Role, s => s.Role)
            .ShallowCopyForSameType(false);

        config.NewConfig<GroupEntity, GroupDto>()
            .MapToConstructor(true)
            .Map(d => d.Identifier, s => s.Identifier)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Members, s => s.Members)
            .Map(d => d.Trainer, s => s.Trainer)
            .ShallowCopyForSameType(false);

        config.NewConfig<GroupMembersEntity, GroupMembersDto>()
            .MapToConstructor(true)
            .Map(d => d.UserId, s => s.UserId)
            .ShallowCopyForSameType(false);

        config.NewConfig<SexType, SexTypeDto>()
                .EnumMappingStrategy(EnumMappingStrategy.ByName)
                .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                .TwoWays();
    }
}
