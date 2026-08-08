using System.ComponentModel.DataAnnotations;
namespace Inventory_Management.ViewModels;

public class FormPermissionModel
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Role is required")]
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Page is required")]
    public int FormId { get; set; }
    public string FormName { get; set; } = string.Empty;
    public bool CanView { get; set; }
    public bool CanCreate { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}
