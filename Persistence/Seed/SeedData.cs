using System.Globalization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTypeEntity>()
            .HasData(
                new UserTypeEntity
                {
                    Id = new Guid("c071c2d4-08b2-4ccb-b816-6e89956660c3"),
                    Name = "Student",
                    Priority = 1,
                    CreatedAt = ConvertStringToDateTime("2024-01-01 13:31:39.855000 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 13:31:39.855000 +00:00")
                }
            );

        modelBuilder.Entity<LocationEntity>()
            .HasData(
                new LocationEntity
                {
                    Id = new Guid("a1cc8e72-a754-4f8c-b2ef-a9b770315aa3"),
                    Location = "AASTU",
                    Country = "Ethiopia",
                    CreatedAt = ConvertStringToDateTime("2024-01-01 13:26:38.118000 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 13:26:38.118000 +00:00")
                }
            );

        modelBuilder.Entity<GroupEntity>()
            .HasData(
                new GroupEntity
                {
                    Id = new Guid("66eb8c85-fb95-46c0-9f34-f860972739b3"),
                    Name = "Group 46",
                    Abbreviation = "G-46",
                    Generation = "4",
                    Year = "2023/24",
                    LocationId = new Guid("a1cc8e72-a754-4f8c-b2ef-a9b770315aa3"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 18:19:28.530000 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 18:19:28.530000 +00:00")
                },
                new GroupEntity
                {
                    Id = new Guid("f178d571-b1a3-4ee3-88b2-c06e4a85ed66"),
                    Name = "Group 45",
                    Abbreviation = "G-45",
                    Generation = "4",
                    Year = "2023/24",
                    LocationId = new Guid("a1cc8e72-a754-4f8c-b2ef-a9b770315aa3"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 18:19:28.530000 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 18:19:28.530000 +00:00")
                }
            );

        modelBuilder.Entity<ContestEntity>()
            .HasData(
                new ContestEntity
                {
                    Id = new Guid("8b378d4b-551a-42ed-970f-686b1e90ba7a"),
                    ContestGlobalId = "495257",
                    ContestUrl = "https://codeforces.com/gym/495257",
                    Name = "Test 2",
                    Type = "",
                    DurationSeconds = 0,
                    StartTimeSeconds = 0,
                    RelativeTimeSeconds = 0,
                    PreparedBy = "",
                    WebsiteUrl = "",
                    Description = "",
                    Difficulty = "",
                    Kind = "",
                    Season = "",
                    Status = "Upcoming",
                    CreatedAt = ConvertStringToDateTime("2024-01-01 12:17:09.186746 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 12:17:09.186750 +00:00")
                },
                new ContestEntity
                {
                    Id = new Guid("451fed4c-889d-4c74-945b-e0659701caac"),
                    ContestGlobalId = "495894",
                    ContestUrl = "https://codeforces.com/gym/495894",
                    Name = "Mikeyas Contest 1",
                    Type = "ICPC",
                    DurationSeconds = 600,
                    StartTimeSeconds = 1704147540,
                    RelativeTimeSeconds = 5459,
                    PreparedBy = "Nahom4258",
                    WebsiteUrl = "",
                    Description = "",
                    Difficulty = "3",
                    Kind = "",
                    Season = "",
                    Status = "FINISHED",
                    CreatedAt = ConvertStringToDateTime("2024-01-01 22:24:49.326402 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 22:24:49.326409 +00:00")
                }
            );

        modelBuilder.Entity<ContestGroupEntity>()
            .HasData(
                new ContestGroupEntity
                {
                    Id = new Guid("a4dcbf5c-c0e0-43ed-966f-979930b3131a"),
                    ContestId = new Guid("8b378d4b-551a-42ed-970f-686b1e90ba7a"),
                    GroupId = new Guid("f178d571-b1a3-4ee3-88b2-c06e4a85ed66"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 12:17:09.492414 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 12:17:09.492415 +00:00")
                },
                new ContestGroupEntity
                {
                    Id = new Guid("112084e2-f424-49eb-9e8d-8bd30b2ef9ec"),
                    ContestId = new Guid("451fed4c-889d-4c74-945b-e0659701caac"),
                    GroupId = new Guid("f178d571-b1a3-4ee3-88b2-c06e4a85ed66"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 22:24:49.700801 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 22:24:49.700801 +00:00")
                }
            );

        modelBuilder.Entity<QuestionEntity>()
            .HasData(
                new QuestionEntity
                {
                    Id = new Guid("18a52ddc-8848-4e34-8d16-6dfc84561129"),
                    GlobalQuestionUrl = "https://codeforces.com/problemset/problem/1861/A",
                    Name = "Prime Deletion",
                    Index = "A",
                    ContestId = new Guid("451fed4c-889d-4c74-945b-e0659701caac"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 22:24:49.700340 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 22:24:49.700340 +00:00")
                },
                new QuestionEntity
                {
                    Id = new Guid("48f0981f-d027-441d-9969-0e9bfed2ade9"),
                    GlobalQuestionUrl = "https://codeforces.com/problemset/problem/1861/B",
                    Name = "Two Binary Strings",
                    Index = "B",
                    ContestId = new Guid("451fed4c-889d-4c74-945b-e0659701caac"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 22:24:49.699943 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 22:24:49.699943 +00:00")
                }
            );

        modelBuilder.Entity<UserEntity>()
            .HasData(
                new UserEntity
                {
                    Id = new Guid("ccfb9dea-0f2c-47c6-b195-1ddcdfb44555"),
                    FirstName = "Haymanot",
                    LastName = "Demis",
                    Email = "haymanotdemis1@gmail.com",
                    UserName = "HaymanotDemis1",
                    CodeforcesHandle = "haymanotdemis",
                    Password = "$2a$11$hxEy3.5H4dv6T2syP3Gtn.DO5qVsgeA19T2Kg0MyIDM7a4/Dj44J2",
                    Token = "57167dc2-295b-4041-8d82-60b508cff699",
                    Gender = "Male",
                    BirthDate = new DateOnly(2001, 5, 9),
                    Phone = "+251939656144",
                    IsVerified = false,
                    ConfirmationCode = null,
                    ConfirmationCodeExpiration = null,
                    NumberOfProblemsSolved = 0,
                    NumberOfProblemsTaken = 0,
                    UserTypeId = new Guid("c071c2d4-08b2-4ccb-b816-6e89956660c3"),
                    GroupId = new Guid("f178d571-b1a3-4ee3-88b2-c06e4a85ed66"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 14:49:45.529755 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 14:49:45.529769 +00:00")
                },
                new UserEntity
                {
                    Id = new Guid("0f027000-f180-48a1-b4d7-3a2d862ed0f9"),
                    FirstName = "Nahom",
                    LastName = "Habtamu",
                    Email = "nhabtamu42@gmail.com",
                    UserName = "Nahom4258",
                    CodeforcesHandle = "Mikeyas",
                    Password = "$2a$11$XVk0kWjTjER9S/6ggkvOmu6FPrdH0Sm6sghYaBQZwNWz/s.XoGXXu",
                    Token = "aa5fdd09-c155-4b15-81e6-707aa393673f",
                    Gender = "Male",
                    BirthDate = new DateOnly(2001, 5, 9),
                    Phone = "+251939656144",
                    IsVerified = false,
                    ConfirmationCode = null,
                    ConfirmationCodeExpiration = null,
                    NumberOfProblemsSolved = 0,
                    NumberOfProblemsTaken = 0,
                    UserTypeId = new Guid("c071c2d4-08b2-4ccb-b816-6e89956660c3"),
                    GroupId = new Guid("f178d571-b1a3-4ee3-88b2-c06e4a85ed66"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 10:32:11.842484 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 10:32:11.842486 +00:00")
                }
            );

        modelBuilder.Entity<UserContestResultEntity>()
            .HasData(
                new UserContestResultEntity
                {
                    Id = new Guid("bffd10dd-b5ee-493d-b85c-aebed4ab15da"),
                    Points = 0,
                    Rank = 1,
                    Penalty = 10,
                    SuccessfulHackCount = 0,
                    UnsuccessfulHackCount = 0,
                    IsVirtual = false,
                    UserId = new Guid("0f027000-f180-48a1-b4d7-3a2d862ed0f9"),
                    ContestId = new Guid("451fed4c-889d-4c74-945b-e0659701caac"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 23:50:00.985568 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 23:50:00.985568 +00:00")
                }
            );

        modelBuilder.Entity<UserQuestionResultEntity>()
            .HasData(
                new UserQuestionResultEntity
                {
                    Id = new Guid("c28e9e09-b136-40d9-9280-ccff82c4bcb9"),
                    Points = 1,
                    RejectedAttemptCount = 0,
                    BestSubmissionTimeSeconds = "302",
                    UserId = new Guid("0f027000-f180-48a1-b4d7-3a2d862ed0f9"),
                    QuestionId = new Guid("18a52ddc-8848-4e34-8d16-6dfc84561129"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 23:50:00.995751 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 23:50:00.995751 +00:00")
                },
                new UserQuestionResultEntity
                {
                    Id = new Guid("9a7cea29-a4d4-493a-8c99-092741254008"),
                    Points = 1,
                    RejectedAttemptCount = 0,
                    BestSubmissionTimeSeconds = "334",
                    UserId = new Guid("0f027000-f180-48a1-b4d7-3a2d862ed0f9"),
                    QuestionId = new Guid("48f0981f-d027-441d-9969-0e9bfed2ade9"),
                    CreatedAt = ConvertStringToDateTime("2024-01-01 23:50:01.000047 +00:00"),
                    ModifiedAt = ConvertStringToDateTime("2024-01-01 23:50:01.000047 +00:00")
                }
            );
    }

    private static DateTime ConvertStringToDateTime(string dateString)
    {
        if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss.ffffff zzz", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTime result))
        {
            return result;
        }
        else
        {
            // Handle the case where the conversion fails
            throw new InvalidOperationException($"Failed to convert string '{dateString}' to DateTime.");
        }
    }
}