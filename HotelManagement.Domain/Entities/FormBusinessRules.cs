namespace HotelManagement.Domain.Entities;

public class FormBusinessRules : Common
{
    private FormBusinessRules()
    {
    }

    public FormBusinessRules(
        int formId,
        string? rules,
        string? rulesCode,
        bool rulesValue,
        int createdBy = 0)
    {
        SetFormId(formId);
        SetRules(rules);
        SetRulesCode(rulesCode);
        SetRulesValue(rulesValue);
        SetCreatedBy(createdBy);
    }

    public int Id { get; private set; }

    public int FormId { get; private set; }

    public string? Rules { get; private set; }

    public string? RulesCode { get; private set; }

    public bool RulesValue { get; private set; }

    #region Private Setters

    private void SetFormId(int formId)
    {
        FormId = formId;
    }

    private void SetRules(string? rules)
    {
        Rules = rules?.Trim();
    }

    private void SetRulesCode(string? rulesCode)
    {
        RulesCode = rulesCode?.Trim().ToUpperInvariant();
    }

    private void SetRulesValue(bool rulesValue)
    {
        RulesValue = rulesValue;
    }

    #endregion

    public void Update(
        int formId,
        string? rules,
        string? rulesCode,
        bool rulesValue,
        int updatedBy)
    {
        SetFormId(formId);
        SetRules(rules);
        SetRulesCode(rulesCode);
        SetRulesValue(rulesValue);

        SetUpdatedBy(updatedBy);
    }

    public void Delete(int updatedBy = 0)
    {
        SoftDelete(updatedBy);
    }
}