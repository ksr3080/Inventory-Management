namespace HotelManagement.Domain.Entities;

public class Company : Common
{
    private Company()
    {

    }
    public Company(string name, string email, string phoneno, string address1, string address2, string state, string statecode,
        string city, string pincode, string gstno, string logo, int createdby)
    {
        SetName(name);
        SetEmail(email);
        SetPhoneNo(phoneno);
        SetAddress1(address1);
        SetAddress2(address2);
        SetState(state);
        SetStateCode(statecode);
        SetCity(city);
        SetPincode(pincode);
        SetGSTNo(gstno);
        SetLogo(logo);
        SetCreatedBy(createdby);
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNo { get; private set; }
    public string Address1 { get; private set; }
    public string? Address2 { get; private set; }
    public string State { get; private set; }
    public string StateCode { get; private set; }
    public string City { get; private set; }
    public string Pincode { get; private set; }
    public string GSTNo { get; private set; }
    public string? Logo { get; private set; }

    private void SetName(string name)
    {
        Name = name.Trim();
    }

    private void SetEmail(string email)
    {
        Email = email.Trim().ToLowerInvariant();
    }

    private void SetPhoneNo(string phoneNo)
    {
        PhoneNo = phoneNo.Trim();
    }

    private void SetAddress1(string address1)
    {
        Address1 = address1.Trim();
    }

    private void SetAddress2(string? address2)
    {
        Address2 = address2?.Trim();
    }

    private void SetState(string state)
    {
        State = state.Trim();
    }

    private void SetStateCode(string stateCode)
    {
        StateCode = stateCode.Trim().ToUpper();
    }

    private void SetCity(string city)
    {
        City = city.Trim();
    }

    private void SetPincode(string pincode)
    {
        Pincode = pincode.Trim();
    }

    private void SetGSTNo(string gstNo)
    {
        GSTNo = gstNo.Trim().ToUpper();
    }
    private void SetLogo(string? logo)
    {
        Logo = logo;
    }

    public void UpdateDetails(string name, string email, string phoneNo, string address1, string? address2, string state, string stateCode,
        string city, string pincode, string gstNo, string? logo, int updatedBy)
    {
        SetName(name);
        SetEmail(email);
        SetPhoneNo(phoneNo);
        SetAddress1(address1);
        SetAddress2(address2);
        SetState(state);
        SetStateCode(stateCode);
        SetCity(city);
        SetPincode(pincode);
        SetGSTNo(gstNo);
        SetLogo(logo);
        SetUpdatedBy(updatedBy);
    }

    public void SoftDelete(int userid)
    {
        base.SoftDelete(userid);
    }
}
