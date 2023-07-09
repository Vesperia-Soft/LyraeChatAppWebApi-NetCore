namespace LyraeChatApp.Domain.Core;

public abstract class EntityBase
{
    public virtual int Id { get; set; }
    public virtual string? CreatorName { get; set; } = "Admin";
    public virtual DateTime? CreatedDate { get; set; } = DateTime.Now;
    public virtual DateTime? UpdateDate { get; set; } = null;
    public virtual string? UpdaterName { get; set; } = null;
    public virtual string? DeleterName { get; set; } = null;
    public virtual DateTime? DeletedTime { get; set; } = null;
    public virtual bool? IsActive { get; set; } = true;
    public virtual bool? IsDelete { get; set; } = false;
}
