using System.Linq.Expressions;
using FilmRating.Persistence.Sql;
using Microsoft.EntityFrameworkCore;

namespace FilmRating.Infrastructure.Repository;

public class Repository<TEntity, T> : IRepository<TEntity, T>
    where TEntity : class, IEntity<T> 
    where T: struct
{
    private readonly FilmRatingDbContext context;
    private readonly DbSet<TEntity> dbSet;

    public Repository(FilmRatingDbContext context)
    {
        this.context = context;
        dbSet = this.context.Set<TEntity>();
    }
    
    public void Create(TEntity item)
    {
        dbSet.Add(item);
        context.SaveChanges();
    }

    public TEntity? FindById(int id)
    {
        return dbSet.Find(id);
    }

    public IEnumerable<TEntity> Get()
    {
        return dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query =  Include(includeProperties);
        return query.Where(predicate).ToList();
    }
    
    public void Remove(TEntity item)
    {
        dbSet.Remove(item);
        context.SaveChanges();
    }

    public void Update(TEntity item)
    {
        context.Entry(item).State = EntityState.Modified;
        context.SaveChanges();
    }
    
    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(
                query, 
                (current, includeProperty) => current.Include(includeProperty));
    }
}