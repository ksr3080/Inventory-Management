namespace Inventory_Management.Domain.Entities;

public abstract class Common
{
    protected Common()
    {
        IsActive = true;
        IsDeleted = false;
        CreatedOn = DateTime.UtcNow;
    }

    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public int? CreatedBy { get; private set; }
    public int? UpdatedBy { get; private set; }

    protected void SetCreatedBy(int? userId)
    {
        CreatedBy = userId;
    }

    protected void SetUpdatedBy(int userId)
    {
        UpdatedBy = userId;
        UpdatedOn = DateTime.UtcNow;
    }

    protected void Activate()
    {
        IsActive = true;
    }

    protected void Deactivate()
    {
        IsActive = false;
    }

    protected void SoftDelete(int userId)
    {
        IsDeleted = true;
        IsActive = false;
        UpdatedBy = userId;
        UpdatedOn = DateTime.UtcNow;
    }
}
