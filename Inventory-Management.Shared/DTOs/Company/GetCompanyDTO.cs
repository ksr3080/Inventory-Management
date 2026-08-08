using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Shared.DTOs.Company;

public class GetCompanyDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public string State { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public string GSTNo { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}
