namespace HotelManagement.Shared.DTOs.FormRulesPermission;

public class GetFormRulesPermissionDTO
{
    public int Id { get; set; }
    public int FormRulesId { get; set; }
    public int FormId { get; set; }
    public int RoleId { get; set; }
    public bool IsAllowRules { get; set; }
}

public class SaveFormRulesPermissionDTO
{
    public int RoleId { get; set; }
    public int CreatedBy { get; set; }
    public List<SavePermissionItemDTO> Permissions { get; set; } = new();
}

public class SavePermissionItemDTO
{
    public int FormRulesId { get; set; }
    public int FormId { get; set; }
    public bool IsAllowRules { get; set; }
}
