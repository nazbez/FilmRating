using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FilmRating.Features.Authentication.UserRoleEntityConstants;

namespace FilmRating.Features.Authentication;

public class RoleEntityMap : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "a77cd0ac-a81a-451e-852f-5061b4a6a2d1",
                Name = Critic,
                NormalizedName = Critic.ToUpper(CultureInfo.InvariantCulture)
            },
            new IdentityRole
            {
                Id = "2b789031-ca50-4855-b335-e2f5cfb47bf4",
                Name = Administrator,
                NormalizedName = Administrator.ToUpper(CultureInfo.InvariantCulture)
            }
        );
    }
}

public static class UserRoleEntityConstants
{
    public const string Critic = nameof(Critic);
    public const string Administrator = nameof(Administrator);
}