using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management.Shared.DTOs.Users;

public class CreateUserDTO
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }
    public string Password { get; set; }

    public string ContactNo { get; set; }

    public bool IsLoginAllowed { get; set; }

    public int CreatedBy { get; set; }
}