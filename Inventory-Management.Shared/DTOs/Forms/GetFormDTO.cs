namespace Inventory_Management.Shared.DTOs.Forms;

public class GetFormDTO
{
    public int Id { get; set; }

    public string FormCode { get; set; }

    public string FormName { get; set; }

    public int? ParentId { get; set; }

    public string? FormUrl { get; set; }

    public string? MenuIconClass { get; set; }

    public bool ShowInMenu { get; set; }

    public bool? IsAddEdit { get; set; }

    public bool? IsView { get; set; }

    public int DisplayIndex { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
}
