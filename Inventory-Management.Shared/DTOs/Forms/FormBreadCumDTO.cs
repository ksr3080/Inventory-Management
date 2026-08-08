namespace Inventory_Management.Shared.DTOs.Forms;

public class FormBreadCumDTO
{
    public int FormId { get; set; }
    public string FormName { get; set; } = string.Empty;
    public string FormCode { get; set; } = string.Empty;
    public int ParentId { get; set; }
    public string BreadCum { get; set; } = string.Empty;
    public string FormUrl { get; set; } = string.Empty;
    public int DisplayIndex { get; set; }
}
