namespace FilmRating.Features.Film;

public class FilmConfiguration
{
    public int MinimumYear { get; set; }
    public decimal MinimumBudget { get; set; }
    public int MinimumDurationInMinutes { get; set; }
    public int DirectorRoleId { get; set; }
    public int ActorRoleId { get; set; }
}