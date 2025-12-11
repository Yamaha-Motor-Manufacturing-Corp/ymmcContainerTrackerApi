using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YmmcContainerTrackerApi.Services;

namespace YmmcContainerTrackerApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IUserService userService, ILogger<IndexModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // User Information
        public string CurrentUser { get; set; } = string.Empty;
        public string UserIdentity { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public bool HasAccess { get; set; }

        // Permissions
        public bool CanViewContainers { get; set; }
        public bool CanEditContainers { get; set; }
        public bool CanAccessReports { get; set; }
        public bool CanAccessMaintenance { get; set; }

        public async Task OnGetAsync()
        {
            // Automatically get Windows authenticated user (no login needed)
            UserIdentity = _userService.GetCurrentUserIdentity();
            CurrentUser = _userService.GetCurrentUsername();

            _logger.LogInformation("User attempted access: {UserIdentity}", UserIdentity);

            // Check if user exists in UserRoles table
            UserRole = await _userService.GetUserRoleAsync(CurrentUser);
            HasAccess = UserRole != "None";

            if (HasAccess)
            {
                // User has access - set their permissions
                CanViewContainers = await _userService.CanViewAsync(CurrentUser);
                CanEditContainers = await _userService.CanEditAsync(CurrentUser);
                CanAccessMaintenance = UserRole is "Admin" or "Editor";
                CanAccessReports = true;

                _logger.LogInformation("Access GRANTED to {CurrentUser} with role: {UserRole}", 
                    CurrentUser, UserRole);
            }
            else
            {
                // User does NOT have access
                _logger.LogWarning("Access DENIED to {CurrentUser} - not found in UserRoles table", 
                    CurrentUser);
            }
        }
    }
}
