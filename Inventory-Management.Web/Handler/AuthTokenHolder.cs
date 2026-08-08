namespace Inventory_Management.Handler;

/// <summary>
/// Scoped service holding the JWT token for the current Blazor Server circuit session.
/// </summary>
public class AuthTokenHolder
{
    public string? Token { get; set; }
}
