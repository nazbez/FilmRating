using System.Linq.Expressions;

namespace FilmRating.Infrastructure.Repository;

public interface IRepository<TEntity, T> 
    where TEntity : class, IEntity<T> 
    where T: struct
{
    void Create(TEntity item);
    TEntity? FindById(int id);
    IEnumerable<TEntity> Get();
    IEnumerable<TEntity> Get(Func<TEntity,bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    void Remove(TEntity item);
    void Update(TEntity item);
}