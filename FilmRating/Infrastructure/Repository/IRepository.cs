using System.Linq.Expressions;

namespace FilmRating.Infrastructure.Repository;

public interface IRepository<TEntity, in T> 
    where TEntity : class, IEntity<T> 
    where T: struct
{
    TEntity? FindById(T id);
    IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null!);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    bool Contains(ISpecification<TEntity> specification = null!);
    bool Contains(Expression<Func<TEntity, bool>> predicate);
    int Count(ISpecification<TEntity> specification = null!);
    int Count(Expression<Func<TEntity, bool>> predicate);
}