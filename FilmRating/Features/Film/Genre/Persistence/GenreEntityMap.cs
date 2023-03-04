using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmRating.Features.Film.Genre;

public class GenreEntityMap : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder.ToTable("Genre");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasData(
            GenreEntity.Create(1, "Action"),
            GenreEntity.Create(2, "Adventure"),
            GenreEntity.Create(3, "Animated"),
            GenreEntity.Create(4, "Comedy"),
            GenreEntity.Create(5, "Drama"),
            GenreEntity.Create(6, "Fantasy"),
            GenreEntity.Create(7, "Historical"),
            GenreEntity.Create(8, "Horror"),
            GenreEntity.Create(9, "Musical"),
            GenreEntity.Create(10, "Noir"),
            GenreEntity.Create(11, "Romance"),
            GenreEntity.Create(12, "Science fiction"),
            GenreEntity.Create(13, "Thriller"),
            GenreEntity.Create(14, "Western"));
    }
}