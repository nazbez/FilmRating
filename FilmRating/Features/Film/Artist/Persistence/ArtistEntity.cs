using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistEntity : IEntity<Guid>
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    public ICollection<ArtistRoleEntity> Roles { get; private set; } = new HashSet<ArtistRoleEntity>();
    public ICollection<FilmEntity> DirectorFilms { get; private set; } = new HashSet<FilmEntity>();
    public ICollection<FilmEntity> ActorFilms { get; private set; } = new HashSet<FilmEntity>();

    public void UpdateFirstName(string firstName) =>
        FirstName = firstName;

    public void UpdateLastName(string lastName) =>
        LastName = lastName;

    public void UpdateRoles(ICollection<ArtistRoleEntity> roles) =>
        Roles = roles;

    public static ArtistEntity Create(string firstName, string lastName, ICollection<ArtistRoleEntity> roles) =>
        new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Roles = roles
        };
}