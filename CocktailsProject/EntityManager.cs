using Microsoft.EntityFrameworkCore;

namespace CocktailsProject;

public class EntityManager
{
    private readonly DatabaseManager _databaseManager;
    
    public EntityManager(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }
    
    public void Add<T>(T entity) where T : class
    {
        _databaseManager.Add(entity);
    }
    
    public void Remove<T>(T entity) where T : class
    {
        _databaseManager.Remove(entity);
    }
    
    public void Update<T>(T entity) where T : class
    {
        _databaseManager.Update(entity);
    }
    
    public T? Get<T>(Func<T, bool> match) where T : class
    {
        return _databaseManager.Get(match);
    }
    
    public List<T> GetAll<T>() where T : class
    {
        return _databaseManager.GetAll<T>();
    }
    
    public DbSet<T> Entity<T>() where T : class
    {
        return _databaseManager.GetDbSet<T>();
    }
}