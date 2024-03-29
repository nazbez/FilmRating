﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmRating.Features.Film.Rating;

public class RatingEntityMap : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> builder)
    {
        builder.ToTable("Rating");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rate);

        builder.HasOne(x => x.Film)
            .WithMany(x => x.Ratings)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User);
    }
}