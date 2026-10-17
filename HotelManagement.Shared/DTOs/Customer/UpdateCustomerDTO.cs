using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Shared.DTOs.Customer;

public class UpdateCustomerDTO
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string GSTNo { get; set; } = string.Empty;

    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }

    public string State { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public string PhoneNo { get; set; } = string.Empty;

    public string Ship_Address1 { get; set; } = string.Empty;
    public string? Ship_Address2 { get; set; }

    public string Ship_State { get; set; } = string.Empty;
    public string Ship_StateCode { get; set; } = string.Empty;
    public string Ship_City { get; set; } = string.Empty;
    public string Ship_Pincode { get; set; } = string.Empty;
    public string Ship_Country { get; set; } = string.Empty;

    public int UpdatedBy { get; set; }
}
