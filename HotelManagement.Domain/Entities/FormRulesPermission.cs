namespace HotelManagement.Domain.Entities;

public class FormRulesPermission : Common
{
    public int Id { get; set; }
    public int FormRulesId { get; set; }
    public int FormId { get; set; }
    public int RoleId { get; set; }
    public bool IsAllowRules { get; set; }
}
