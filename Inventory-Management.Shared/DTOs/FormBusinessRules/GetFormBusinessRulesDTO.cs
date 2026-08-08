namespace Inventory_Management.Shared.DTOs.FormBusinessRules;

public class GetFormBusinessRulesDTO
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public string? Rules { get; set; }

    public string? RulesCode { get; set; }

    public bool RulesValue { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
}
