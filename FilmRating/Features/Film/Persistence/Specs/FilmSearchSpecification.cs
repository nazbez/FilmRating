using System.Globalization;
using System.Linq.Expressions;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmSearchSpecification : BaseSpecification<FilmEntity>
{
    public FilmSearchSpecification(string text, string? sortColumn, bool? isAscending, int? page = null, int? pageSize = null) 
        : base(x => 
            x.Title.Contains(text))
    {
        AddInclude(x => x.Genre!);

        if (isAscending.HasValue)
        {
            var sortingExpression = GetSortingExpression(sortColumn);

            if (isAscending.Value)
            {
                ApplyOrderBy(sortingExpression);
            }
            else
            {
                ApplyOrderByDescending(sortingExpression);
            }
        }

        if (page.HasValue && pageSize.HasValue)
        {
            ApplyPaging(page.Value * pageSize.Value, pageSize.Value);
        }
    }

    public FilmSearchSpecification AddPaging(int page, int pageSize)
    {
        ApplyPaging(page * pageSize, pageSize);
        return this;
    }
    
    private static Expression<Func<FilmEntity, object>> GetSortingExpression(string? sortColumn) => 
        sortColumn?.ToLower(CultureInfo.CurrentCulture) switch 
        { 
            "title" => film => film.Title, 
            "genre" => film => (string)film.Genre!, 
            "year" => film => film.Year, 
            "rating" => film => film.Rating, 
            _ => film => film.Id 
        };
}