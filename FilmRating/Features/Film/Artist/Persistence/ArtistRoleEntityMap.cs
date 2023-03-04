using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            ArtistRoleEntity.Create(1, "Director"),
            ArtistRoleEntity.Create(2, "Actor"));
    }
}