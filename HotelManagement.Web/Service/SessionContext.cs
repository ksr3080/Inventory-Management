using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using HotelManagement.Handler;
using HotelManagement.Interface;
using HotelManagement.Shared.DTOs.Auth;
using HotelManagement.Shared.DTOs.FormRulesPermission;
using HotelManagement.Shared.DTOs.Forms;
using HotelManagement.Shared.Helper.FormAccessCode;

namespace HotelManagement.Service;

public class PersistedSessionModel
{
    public LoginResponseDTO Response { get; set; } = new();
    public DateTime LastActivityTime { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Stores logged-in user session and all role permissions.
/// Session is stored in ProtectedSessionStorage and automatically expires after 30 minutes of inactivity
/// or when the project/server restarts.
/// </summary>
public class SessionContext
{
    private readonly ProtectedSessionStorage _storage;
    private readonly IFormService _formService;
    private readonly IFormRulesPermissionService _permService;
    private readonly IFormBusinessRulesService _ruleService;
    private readonly AuthTokenHolder _tokenHolder;

    // 30 Minutes Inactivity Timeout
    private static readonly TimeSpan SessionTimeout = TimeSpan.FromMinutes(30);

    public SessionContext(
        ProtectedSessionStorage storage,
        IFormService formService,
        IFormRulesPermissionService permService,
        IFormBusinessRulesService ruleService,
        AuthTokenHolder tokenHolder)
    {
        _storage = storage;
        _formService = formService;
        _permService = permService;
        _ruleService = ruleService;
        _tokenHolder = tokenHolder;
    }

    // ── User Session ──
    public bool IsAuthenticated { get; private set; }
    public int UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public int RoleId { get; private set; }
    public int CompanyId { get; private set; }
    public string Token { get; private set; } = string.Empty;

    // ── Internal Data ──
    private readonly Dictionary<int, HashSet<string>> _permissions = new();
    private readonly Dictionary<string, int> _formCodeToId = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<int, bool> _formShowInMenu = new();

    // ── Save Session ──
    public async Task SaveSessionAsync(LoginResponseDTO response)
    {
        SetSession(response);
        try
        {
            var model = new PersistedSessionModel
            {
                Response = response,
                LastActivityTime = DateTime.UtcNow
            };
            await _storage.SetAsync("user_session", model);
        }
        catch
        {
            // Ignore storage errors if browser blocks storage
        }
    }

    // ── Set Session Memory ──
    public void SetSession(LoginResponseDTO response)
    {
        IsAuthenticated = true;
        UserId = response.UserId;
        UserName = response.UserName;
        RoleId = response.RoleId;
        CompanyId = response.CompanyId;
        Token = response.Token ?? string.Empty;
        _tokenHolder.Token = response.Token;
    }

    // ── Try Restoring Session with 30-Minute Timeout & Server Restart Check ──
    public async Task<bool> TryRestoreSessionAsync()
    {
        try
        {
            var result = await _storage.GetAsync<PersistedSessionModel>("user_session");
            if (result.Success && result.Value?.Response != null && !string.IsNullOrEmpty(result.Value.Response.Token))
            {
                var model = result.Value;

                // Check 30 Minutes Timeout
                if (DateTime.UtcNow - model.LastActivityTime > SessionTimeout)
                {
                    await ClearAsync();
                    return false;
                }

                // Update last activity timestamp to slide 30-minute window
                model.LastActivityTime = DateTime.UtcNow;
                try
                {
                    await _storage.SetAsync("user_session", model);
                }
                catch { }

                SetSession(model.Response);

                var forms = await _formService.GetFormsAsync();
                LoadForms(forms);

                var permissions = await _permService.GetByRoleIdAsync(model.Response.RoleId);
                var businessRules = await _ruleService.GetFormBusinessRulesAsync();
                LoadPermissions(permissions, businessRules);

                return true;
            }
        }
        catch
        {
            // Execution throws if project/server stopped/restarted or payload is invalid
            await ClearAsync();
        }

        return false;
    }

    // ── Load Forms ──
    public void LoadForms(IEnumerable<GetFormDTO> forms)
    {
        _formCodeToId.Clear();
        _formShowInMenu.Clear();

        foreach (var form in forms)
        {
            if (!string.IsNullOrWhiteSpace(form.FormCode))
            {
                _formCodeToId[form.FormCode.Trim()] = form.Id;
            }
            _formShowInMenu[form.Id] = form.ShowInMenu;
        }
    }

    // ── Load Permissions ──
    public void LoadPermissions(
        IEnumerable<GetFormRulesPermissionDTO> permissions,
        IEnumerable<HotelManagement.Shared.DTOs.FormBusinessRules.GetFormBusinessRulesDTO> businessRules)
    {
        _permissions.Clear();

        var ruleCodeMap = businessRules.ToDictionary(r => r.Id, r => r.RulesCode ?? string.Empty);

        foreach (var perm in permissions)
        {
            if (!perm.IsAllowRules) continue;

            if (!ruleCodeMap.TryGetValue(perm.FormRulesId, out var ruleCode) || string.IsNullOrWhiteSpace(ruleCode))
                continue;

            if (!_permissions.TryGetValue(perm.FormId, out var ruleSet))
            {
                ruleSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                _permissions[perm.FormId] = ruleSet;
            }

            ruleSet.Add(ruleCode.Trim());
        }
    }

    // ── Check Permission ──
    public bool HasPermission(FormAccessCode formCode, string ruleCode)
    {
        if (string.IsNullOrWhiteSpace(ruleCode)) return false;
        if (!_formCodeToId.TryGetValue(formCode.ToString(), out var formId)) return false;
        if (!_permissions.TryGetValue(formId, out var rules)) return false;

        return rules.Contains(ruleCode.Trim());
    }

    // ── Check Permission by FormId ──
    public bool HasPermissionByFormId(int formId, string ruleCode)
    {
        if (string.IsNullOrWhiteSpace(ruleCode)) return false;
        if (!_permissions.TryGetValue(formId, out var rules)) return false;
        return rules.Contains(ruleCode.Trim());
    }

    // ── Clear / Logout ──
    public async Task ClearAsync()
    {
        IsAuthenticated = false;
        UserId = 0;
        UserName = string.Empty;
        RoleId = 0;
        CompanyId = 0;
        Token = string.Empty;
        _tokenHolder.Token = null;
        _permissions.Clear();
        _formCodeToId.Clear();
        _formShowInMenu.Clear();

        try
        {
            await _storage.DeleteAsync("user_session");
        }
        catch
        {
            // Ignore storage deletion errors
        }
    }
}
