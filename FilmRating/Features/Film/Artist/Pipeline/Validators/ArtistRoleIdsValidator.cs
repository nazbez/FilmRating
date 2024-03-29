﻿using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.Artist;

public class ArtistRoleIdsValidator : AbstractValidator<IEnumerable<int>>
{
    private const string ErrorMessage = "Artist roles ids must be between 1 and 2";
    
    public ArtistRoleIdsValidator(IRepository<ArtistRoleEntity, int> artistRoleRepository)
    {
        RuleFor(roleIds => roleIds)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(roleIds => roleIds)
                    .Must(roleIds => 
                        artistRoleRepository.Contains(new ArtistRoleGetByIdsSpecification(roleIds)))
                    .WithMessage(ErrorMessage);
            });
    }
}