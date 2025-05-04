using Microsoft.EntityFrameworkCore;

namespace MyApp.Data;

public class RealmDbContextFactory : IRealmDbContextFactory
{
    private readonly IConfiguration _configuration;

    public RealmDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext CreateDbContext(string realm)
    {
        var dbFolder = _configuration["DbFolder"] ?? "Databases";
        Directory.CreateDirectory(dbFolder); // ensure folder exists

        var connectionString = $"Data Source={Path.Combine(dbFolder, $"{realm}.db")}";
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .Options;

        var db = new AppDbContext(options);
        db.Database.EnsureCreated(); // ensure DB/tables exist
        return db;
    }
}
