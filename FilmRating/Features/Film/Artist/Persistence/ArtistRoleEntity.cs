using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistRoleEntity : IEntity<int>
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;

    public ICollection<ArtistEntity> Artists { get; private set; } = new HashSet<ArtistEntity>();

    public static ArtistRoleEntity Create(int id, string name) =>
        new()
        {
            Id = id,
            Name = name
        };
}