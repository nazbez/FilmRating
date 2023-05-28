using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FilmRating.Features.Film.Artist.ArtistRoleEntityConstants;

namespace FilmRating.Features.Film.Artist;

public class ArtistRoleEntityMap : IEntityTypeConfiguration<ArtistRoleEntity>
{
    public void Configure(EntityTypeBuilder<ArtistRoleEntity> builder)
    {
        builder.ToTable("ArtistRole");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name)
            .IsRequired();
                
        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasData(
            ArtistRoleEntity.Create(DirectorId, "Director"),
            ArtistRoleEntity.Create(ActorId, "Actor"));
    }
}

public static class ArtistRoleEntityConstants
{
    public const int DirectorId = 1;
    public const int ActorId = 2;
}