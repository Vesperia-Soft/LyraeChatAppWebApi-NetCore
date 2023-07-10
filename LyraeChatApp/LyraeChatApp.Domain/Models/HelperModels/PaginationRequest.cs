namespace LyraeChatApp.Domain.Models.HelperModels;

public class PaginationRequest
{
    public virtual int PageNumber { get; set; } = 1;
    public virtual int PageSize { get; set; } = 10;
}
