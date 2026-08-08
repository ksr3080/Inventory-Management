namespace Inventory_Management.Domain.Entities;

public class Users : Common
{
    private Users()
    {
    }

    public Users(
        string firstName,
        string lastName,
        string userName,
        string emailId,
        string password,
        string? contactNo,
        bool isLoginAllowed,
        int? userTypeId,
        string? userType,
        int? createdBy)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetUserName(userName);
        SetEmailId(emailId);
        SetPassword(password);
        SetContactNo(contactNo);
        SetIsLoginAllowed(isLoginAllowed);
        SetUserType(userTypeId, userType);
        SetCreatedBy(createdBy);
    }

    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string UserName { get; private set; }

    public string EmailId { get; private set; }

    public string Password { get; private set; }

    public string? ContactNo { get; private set; }

    public bool IsLoginAllowed { get; private set; }

    public int? UserTypeId { get; private set; }

    public string? UserType { get; private set; }

    #region Private Setters

    private void SetFirstName(string firstName)
    {
        FirstName = firstName.Trim();
    }

    private void SetLastName(string lastName)
    {
        LastName = lastName.Trim();
    }

    private void SetUserName(string userName)
    {
        UserName = userName.Trim();
    }

    private void SetEmailId(string emailId)
    {
        EmailId = emailId.Trim().ToLowerInvariant();
    }

    private void SetPassword(string password)
    {
        Password = password;
    }

    private void SetContactNo(string? contactNo)
    {
        ContactNo = contactNo?.Trim();
    }

    private void SetIsLoginAllowed(bool isLoginAllowed)
    {
        IsLoginAllowed = isLoginAllowed;
    }

    private void SetUserType(int? userTypeId, string? userType)
    {
        UserTypeId = userTypeId;
        UserType = userType?.Trim();
    }

    #endregion

    public void UpdateDetails(
        string firstName,
        string lastName,
        string userName,
        string emailId,
        string? contactNo,
        bool isLoginAllowed,
        int userTypeId,
        string? userType,
        int updatedBy)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetUserName(userName);
        SetEmailId(emailId);
        SetContactNo(contactNo);
        SetIsLoginAllowed(isLoginAllowed);
        SetUserType(userTypeId, userType);
        SetUpdatedBy(updatedBy);
    }

    public void ChangePassword(string password, int updatedBy)
    {
        SetPassword(password);
        SetUpdatedBy(updatedBy);
    }

    public void Delete(int updatedBy)
    {
        SoftDelete(updatedBy);
    }
}