namespace MyApp.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
}


// dotnet ef migrations add InitialCreate --project MyApp.csproj
