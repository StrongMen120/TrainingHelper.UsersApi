# Magistry-Back

## Updating Database

```
dotnet ef migrations add AddedInfoWhoAddedEntities --project .\src\Training.API.Users.Database\Training.API.Users.Database.csproj --context TrainingUsersDbContext --startup-project .\src\Training.API.Users\Training.API.Users.csproj
```

```
dotnet ef migrations remove --project ./src/Training.API.Users.Database/Training.API.Users.Database.csproj --context TrainingUsersDbContext --startup-project ./src/Training.API.Users/Training.API.Users.csproj
```

dotnet ef database update InitialCreation --project ./src/Training.API.Users.Database/Training.API.Users.Database.csproj --context TrainingUsersDbContext --startup-project ./src/Training.API.Users/Training.API.Users.csproj

```

```
