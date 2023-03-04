using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmRating.Features.Film.Artist;

public class ArtistEntityMap : IEntityTypeConfiguration<ArtistEntity>
{
    public void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.ToTable("Artist");
                
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .IsRequired();

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Artists)
            .UsingEntity(x => x.ToTable("Artist_ArtistRole"));
    }
}