namespace YmmcContainerTrackerApi.Services;

public interface IUserService
{
    
    /// Gets current logged-in username (extracts from DOMAIN\username format)
    string GetCurrentUsername();


    /// Gets current user's full identity (DOMAIN\username)
    string GetCurrentUserIdentity();

    /// Checks if user can edit/create/delete containers
    Task<bool> CanEditAsync(string username);


    /// Checks if user can view containers
    Task<bool> CanViewAsync(string username);


    /// Gets user's role (Admin, Editor, Viewer, None)
    Task<string> GetUserRoleAsync(string username);

    /// Gets user's display information
    Task<UserDisplayInfo> GetUserDisplayInfoAsync(string username);


    /// Checks if authentication is enabled
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
