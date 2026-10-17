namespace HotelManagement.Shared.DTOs.FormBusinessRules;

public class UpdateFormBusinessRulesDTO
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public string? Rules { get; set; }

    public string? RulesCode { get; set; }

    public bool RulesValue { get; set; }

    public int UpdatedBy { get; set; }
}
