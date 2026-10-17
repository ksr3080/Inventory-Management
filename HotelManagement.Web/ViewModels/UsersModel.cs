using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels;

public class UsersModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required.")]
    public string ContactNo { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select role.")]
    public int RoleId { get; set; }
    public bool IsActive { get; set; } = true;

    // Nullable: only required when adding a new user or when ResetPassword is checked
    public string? Password { get; set; }

    // Nullable: only required when adding a new user or when ResetPassword is checked
    public string? ConfirmPassword { get; set; }

    /// <summary>Tracks the 'Reset Password?' checkbox state (edit mode only).</summary>
    public bool ResetPassword { get; set; } = false;

    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

