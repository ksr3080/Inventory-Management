namespace Inventory_Management.Shared.DTOs.FormBusinessRules;

public class CreateFormBusinessRulesDTO
{
    public int FormId { get; set; }

    public string? Rules { get; set; }

    public string? RulesCode { get; set; }

    public bool RulesValue { get; set; }

    public int CreatedBy { get; set; }
}
