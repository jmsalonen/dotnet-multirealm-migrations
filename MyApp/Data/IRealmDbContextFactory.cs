namespace MyApp.Data;

public interface IRealmDbContextFactory
{
    AppDbContext CreateDbContext(string realm);
}
