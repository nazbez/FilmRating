namespace FilmRating.Infrastructure.Repository;

public interface IEntity<out T>
    where T : struct
{
    T Id { get; }
}

[ConfigurableEntity]
public abstract class Entity<T> : IEntity<T>
    where T : struct
{
    public T Id { get; protected set; }
}