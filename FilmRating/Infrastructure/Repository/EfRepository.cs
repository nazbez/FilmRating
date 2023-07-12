using System.Linq.Expressions;
using FilmRating.Persistence.Sql;
using Microsoft.EntityFrameworkCore;

namespace FilmRating.Infrastructure.Repository;

public class EfRepository<TEntity, T> : IRepository<TEntity, T>
    where TEntity : class, IEntity<T> 
    where T: struct
{
    private readonly FilmRatingDbContext context;

    public EfRepository(FilmRatingDbContext context)
    {
        this.context = context;
    }

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().AddRange(entities);
    }
    
    public bool Contains(ISpecification<TEntity> specification = null!)
    {
        return Count(specification) > 0;
    }

    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return Count(predicate) > 0;
    }

    public int Count(ISpecification<TEntity> specification = null!)
    {
        return ApplySpecification(specification).Count();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return context.Set<TEntity>().Where(predicate).Count();
    }
    
    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null!)
    {
        return ApplySpecification(specification);
    }

    public TEntity? FindById(T id)
    {
        return context.Set<TEntity>().Find(id);
    }
    
    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return new SpecificationEvaluator<TEntity, T>()
            .GetQuery(context.Set<TEntity>().AsQueryable(), spec);
    }
}