using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmRating.Features.Film;

public class FilmEntityMap : IEntityTypeConfiguration<FilmEntity>
{
    public void Configure(EntityTypeBuilder<FilmEntity> builder)
    {
        builder.ToTable("Film");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.ShortDescription)
            .IsRequired();

        builder.Property(x => x.Budget)
            .IsRequired();
        
        builder.Property(x => x.Duration)
            .IsRequired();
        
        builder.Property(x => x.Rating)
            .HasDefaultValue(0.0);

        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Films)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Director)
            .WithMany(x => x.DirectorFilms)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Actors)
            .WithMany(x => x.ActorFilms)
            .UsingEntity(x => x.ToTable("Film_Actor"));
    }
}