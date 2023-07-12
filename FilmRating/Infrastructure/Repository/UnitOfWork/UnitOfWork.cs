using System.Collections;
using FilmRating.Persistence.Sql;

namespace FilmRating.Infrastructure.Repository;

public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    IRepository<TEntity, T> Repository<TEntity, T>() where TEntity : class, IEntity<T> where T: struct;
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly FilmRatingDbContext context;
    private Hashtable? repositories;
    private bool disposed;

    public UnitOfWork(FilmRatingDbContext context)
    {
        this.context = context;
    }
    
    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public IRepository<TEntity, T> Repository<TEntity, T>() where TEntity : class, IEntity<T> where T : struct
    {
        repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (repositories.ContainsKey(type)) 
            return (IRepository<TEntity, T>)repositories[type]!;
        
        var repositoryType = typeof(EfRepository<TEntity, T>);

        var repositoryInstance =
            Activator.CreateInstance(repositoryType, context);

        repositories.Add(type, repositoryInstance);

        return (IRepository<TEntity, T>)repositories[type]!;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            
            disposed = true;
        }
    }
}

