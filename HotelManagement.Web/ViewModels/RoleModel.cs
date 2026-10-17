using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels;

public class RoleModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Role name is required")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RoleLevel { get; set; }
    public bool IsActive { get; set; } = true;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}
