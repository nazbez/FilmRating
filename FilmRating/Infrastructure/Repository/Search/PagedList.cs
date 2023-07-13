namespace FilmRating.Infrastructure.Repository;

public class PagedList<T>
{
    public PagedList(ICollection<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public ICollection<T> Items { get; }
    public int Page { get;}
    public int PageSize { get;}
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}