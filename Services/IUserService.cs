namespace YmmcContainerTrackerApi.Services;

public interface IUserService
{
    /// <summary>
    /// Gets current logged-in username (extracts from DOMAIN\username format)
    /// </summary>
    string GetCurrentUsername();

    /// <summary>
    /// Gets current user's full identity (DOMAIN\username)
    /// </summary>
    string GetCurrentUserIdentity();

    /// <summary>
    /// Checks if user can edit/create/delete containers
    /// </summary>
    Task<bool> CanEditAsync(string username);

    /// <summary>
    /// Checks if user can view containers
    /// </summary>
    Task<bool> CanViewAsync(string username);

    /// <summary>
    /// Gets user's role (Admin, Editor, Viewer, None)
    /// </summary>
    Task<string> GetUserRoleAsync(string username);

    /// <summary>
    /// Gets user's display information
    /// </summary>
    Task<UserDisplayInfo> GetUserDisplayInfoAsync(string username);

    /// <summary>
    /// Checks if authentication is enabled
    /// </summary>
    bool IsAuthenticationEnabled { get; }
}

public class UserDisplayInfo
{
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool CanEdit { get; set; }
    public bool CanView { get; set; }
}
