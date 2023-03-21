using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CocktailsProject;

public class DatabaseManager
{
    private readonly string _connectionString;
    
    public DatabaseManager(IConfiguration configuration)
    {
        // Get the database configuration from appsettings.json
        var section = configuration.GetSection("Database");
        var user = section["DB_USER"];
        var password = section["DB_PASSWORD"];
        var host = section["DB_HOST"];
        var port = section["DB_PORT"];
        var database = section["DB_NAME"];

        // Create the connection string
        _connectionString = $"User ID={user};Password={password};Host={host};Port={port};Database={database};Pooling=true;";
    }
    
    private CocktailsContext GetContext()
    {
        // Create the database context
        var optionsBuilder = new DbContextOptionsBuilder<CocktailsContext>();
        optionsBuilder.UseNpgsql(_connectionString);
        return new CocktailsContext(optionsBuilder.Options);
    }
    
    public void UseContext(Action<CocktailsContext> action)
    {
        // Use the database context
        using var context = GetContext();
        action(context);
        
        // Save the changes
        context.SaveChanges();
    }
    
    public void Add<T>(T entity) where T : class
    {
        // Add an entity to the database
        UseContext(context => context.Add(entity));
    }
    
    public void Remove<T>(T entity) where T : class
    {
        // Remove an entity from the database
        UseContext(context => context.Remove(entity));
    }

    public void Update<T>(T entity) where T : class
    {
        UseContext(context => context.Update(entity));
    }
    
    public T? Get<T>(Func<T, bool> match) where T : class
    {
        // Get an entity from the database
        using var context = GetContext();
        return context.Set<T>().FirstOrDefault(match);
    }
    
    public List<T> GetAll<T>() where T : class
    {
        // Get all entities from the database
        using var context = GetContext();
        return context.Set<T>().ToList();
    }
    
    public DbSet<T> GetDbSet<T>() where T : class
    {
        // Get the database set for an entity
        var context = GetContext();
        return context.Set<T>();
    }
}