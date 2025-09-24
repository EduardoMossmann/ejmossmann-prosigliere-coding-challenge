dotnet restore
dotnet build
dotnet ef database update --project "BloggingPlatform.Infrastructure/BloggingPlatform.Infrastructure.Data.csproj" --startup-project "BloggingPlatform.Api/BloggingPlatform.Api.csproj"
dotnet run --project "BloggingPlatform.Api/BloggingPlatform.Api.csproj"