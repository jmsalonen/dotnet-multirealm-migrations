namespace MyApp.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
}


// dotnet ef migrations add NameOfNewMigration --project MyApp.csproj
