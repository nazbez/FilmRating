using System.Linq.Expressions;

namespace FilmRating.Infrastructure.Repository;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    ICollection<Expression<Func<T, object>>> Includes { get; }
    ICollection<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}