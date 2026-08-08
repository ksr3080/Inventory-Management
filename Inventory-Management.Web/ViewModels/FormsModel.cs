using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.ViewModels;

public class FormsModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Form code is required")]
    public string FormCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Form name is required")]
    public string FormName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Route is required")]
    public string Route { get; set; } = string.Empty;
    public int ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int DisplayIndex { get; set; }
    public string Icon { get; set; } = string.Empty;
    public bool ShowInMenu { get; set; } = true;
    public bool IsAddEdit { get; set; }
    public bool IsView { get; set; }
    public bool IsActive { get; set; } = true;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

