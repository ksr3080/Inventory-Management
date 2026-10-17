namespace HotelManagement.Shared.DTOs.Forms;

public class CreateFormDTO
{
    public string FormCode { get; set; }

    public string FormName { get; set; }

    public int ParentId { get; set; }

    public string? FormUrl { get; set; }

    public string? MenuIconClass { get; set; }

    public bool ShowInMenu { get; set; }

    public bool IsAddEdit { get; set; }

    public bool IsView { get; set; }

    public int DisplayIndex { get; set; }

    public int CreatedBy { get; set; }
}
