namespace HotelManagement.Domain.Entities;

public class Forms : Common
{
    private Forms()
    {
    }

    public Forms(
        string formCode,
        string formName,
        int parentId,
        string? formUrl,
        string? menuIconClass,
        bool showInMenu,
        bool isAddEdit,
        bool isView,
        int displayIndex,
        int createdBy = 0)
    {
        SetFormCode(formCode);
        SetFormName(formName);
        SetParentId(parentId);
        SetFormUrl(formUrl);
        SetMenuIconClass(menuIconClass);
        SetShowInMenu(showInMenu);
        SetIsAddEdit(isAddEdit);
        SetIsView(isView);
        SetDisplayIndex(displayIndex);
        SetCreatedBy(createdBy);
    }

    public int Id { get; private set; }

    public string FormCode { get; private set; }

    public string FormName { get; private set; }

    public int? ParentId { get; private set; }

    public string FormUrl { get; private set; }

    public string? MenuIconClass { get; private set; }

    public bool ShowInMenu { get; private set; }

    public bool? IsAddEdit { get; private set; }

    public bool? IsView { get; private set; }

    public int DisplayIndex { get; private set; }

    #region Private Setters

    private void SetFormCode(string formCode)
    {
        FormCode = formCode.Trim().ToUpperInvariant();
    }

    private void SetFormName(string formName)
    {
        FormName = formName.Trim();
    }

    private void SetParentId(int parentId)
    {
        ParentId = parentId;
    }

    private void SetFormUrl(string formUrl)
    {
        FormUrl = formUrl?.Trim();
    }

    private void SetMenuIconClass(string? menuIconClass)
    {
        MenuIconClass = menuIconClass?.Trim();
    }

    private void SetShowInMenu(bool showInMenu)
    {
        ShowInMenu = showInMenu;
    }

    private void SetIsAddEdit(bool? isAddEdit)
    {
        IsAddEdit = isAddEdit;
    }

    private void SetIsView(bool? isView)
    {
        IsView = isView;
    }

    private void SetDisplayIndex(int displayIndex)
    {
        DisplayIndex = displayIndex;
    }

    #endregion

    public void Delete(int updatedBy)
    {
        SoftDelete(updatedBy);
    }

    public void UpdateDetails(
        string formCode,
        string formName,
        int parentId,
        string formUrl,
        string? menuIconClass,
        bool showInMenu,
        bool? isAddEdit,
        bool? isView,
        int displayIndex,
        int updatedBy)
    {
        SetFormCode(formCode);
        SetFormName(formName);
        SetParentId(parentId);
        SetFormUrl(formUrl);
        SetMenuIconClass(menuIconClass);
        SetShowInMenu(showInMenu);
        SetIsAddEdit(isAddEdit);
        SetIsView(isView);
        SetDisplayIndex(displayIndex);

        SetUpdatedBy(updatedBy);
    }
}