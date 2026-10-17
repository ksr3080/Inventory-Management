namespace HotelManagement.Domain.Entities;

public class Customer : Common
{
    private Customer()
    {
    }

    public Customer(string code, string email, string gstNo, string address1, string? address2, string state, string stateCode, string city, string pincode,
        string country, string phoneNo, string shipAddress1, string? shipAddress2, string shipState, string shipStateCode, string shipCity, string shipPincode, string shipCountry, int createdBy = 0)
    {
        SetCode(code);
        SetEmail(email);
        SetGSTNo(gstNo);
        SetAddress1(address1);
        SetAddress2(address2);
        SetState(state);
        SetStateCode(stateCode);
        SetCity(city);
        SetPincode(pincode);
        SetCountry(country);
        SetPhoneNo(phoneNo);

        SetShipAddress1(shipAddress1);
        SetShipAddress2(shipAddress2);
        SetShipState(shipState);
        SetShipStateCode(shipStateCode);
        SetShipCity(shipCity);
        SetShipPincode(shipPincode);
        SetShipCountry(shipCountry);
        SetCreatedBy(createdBy);
    }
    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Email { get; private set; }
    public string GSTNo { get; private set; }
    public string Address1 { get; private set; }
    public string Address2 { get; private set; }
    public string State { get; private set; }
    public string StateCode { get; private set; }
    public string City { get; private set; }
    public string Pincode { get; private set; }
    public string Country { get; private set; }
    public string PhoneNo { get; private set; }
    public string Ship_Address1 { get; private set; }
    public string Ship_Address2 { get; private set; }
    public string Ship_State { get; private set; }
    public string Ship_StateCode { get; private set; }
    public string Ship_City { get; private set; }
    public string Ship_Pincode { get; private set; }
    public string Ship_Country { get; private set; }
    private void SetCode(string code)
    {
        Code = code.Trim().ToUpper();
    }

    private void SetEmail(string email)
    {
        Email = email.Trim().ToLowerInvariant();
    }

    private void SetGSTNo(string gstNo)
    {
         GSTNo = gstNo.Trim().ToUpper();
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

    private void SetCountry(string country)
    {
        Country = country.Trim();
    }

    private void SetPhoneNo(string phoneNo)
    {
        PhoneNo = phoneNo.Trim();
    }

    private void SetShipAddress1(string address)
    {
         Ship_Address1 = address.Trim();
    }

    private void SetShipAddress2(string? address)
    {
        Ship_Address2 = address?.Trim();
    }

    private void SetShipState(string state)
    {
        Ship_State = state.Trim();
    }

    private void SetShipStateCode(string stateCode)
    {        
        Ship_StateCode = stateCode.Trim().ToUpper();
    }

    private void SetShipCity(string city)
    {
        Ship_City = city.Trim();
    }

    private void SetShipPincode(string pincode)
    {
        Ship_Pincode = pincode.Trim();
    }

    private void SetShipCountry(string country)
    {   
        Ship_Country = country.Trim();
    }
    public void UpdateDetails(string code, string email, string gstNo, string address1, string? address2, string state, string stateCode, string city, string pincode,
        string country, string phoneNo, string shipAddress1, string? shipAddress2, string shipState, string shipStateCode, string shipCity, string shipPincode, string shipCountry, int updatedBy)
    {
        SetCode(code);
        SetEmail(email);
        SetGSTNo(gstNo);
        SetAddress1(address1);
        SetAddress2(address2);
        SetState(state);
        SetStateCode(stateCode);
        SetCity(city);
        SetPincode(pincode);
        SetCountry(country);
        SetPhoneNo(phoneNo);

        SetShipAddress1(shipAddress1);
        SetShipAddress2(shipAddress2);
        SetShipState(shipState);
        SetShipStateCode(shipStateCode);
        SetShipCity(shipCity);
        SetShipPincode(shipPincode);
        SetShipCountry(shipCountry);
        SetUpdatedBy(updatedBy);
    }

}
