namespace FilmRating.Infrastructure.Repository;

public interface IEntity { }

public interface IEntity<out T>  : IEntity
    where T : struct
{
    T Id { get; }
}