namespace LyraeChatApp.Domain.Helpers;

public class PaginationHelper<T>
{
    public int TotalCount { get; set; } 
    public int PageSize { get;set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public List<T> Items { get; set; }
    public PaginationHelper(int totalCount, int pageSize, int currentPage, List<T> items)
    {
        TotalCount= totalCount;
        PageSize= pageSize;
        CurrentPage = currentPage;
        Items = items;
    }

}
