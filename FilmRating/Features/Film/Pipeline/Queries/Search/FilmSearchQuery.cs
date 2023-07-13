using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmSearchQuery(string? Text, string? SortColumn, bool? IsAscending, int Page = 1, int PageSize = 5) 
    : IRequest<PagedList<FilmSearchResultModel>>;

[UsedImplicitly]
public class FilmSearchQueryHandler : IRequestHandler<FilmSearchQuery, PagedList<FilmSearchResultModel>>
{
    private readonly IRepository<FilmEntity, int> filmRepository;
    private readonly IMapper mapper;

    public FilmSearchQueryHandler(IRepository<FilmEntity, int> filmRepository, IMapper mapper)
    {
        this.filmRepository = filmRepository;
        this.mapper = mapper;
    }

    public Task<PagedList<FilmSearchResultModel>> Handle(FilmSearchQuery request, CancellationToken cancellationToken)
    {
        var (text, sortColumn, isAscending, page, pageSize) = request;
        
        var searchSpec = new FilmSearchSpecification(text ?? string.Empty, sortColumn, isAscending);
        
        var totalCount = filmRepository.Count(searchSpec);

        var searchResult = filmRepository.Find(searchSpec.AddPaging(page, pageSize));

        var items = mapper.Map<ICollection<FilmSearchResultModel>>(searchResult);

        var pagedList = new PagedList<FilmSearchResultModel>(items, page, pageSize, totalCount);

        return Task.FromResult(pagedList);
    }
}