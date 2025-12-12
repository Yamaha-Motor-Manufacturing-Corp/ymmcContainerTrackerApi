using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YmmcContainerTrackerApi.Models;
using YmmcContainerTrackerApi.Services;

namespace YmmcContainerTrackerApi.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IUserService userService, 
            IAuditService auditService,
            ILogger<IndexModel> logger)
        {
            _userService = userService;
            _auditService = auditService;
            _logger = logger;
        }

        public string CurrentUser { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public bool HasAccess { get; set; }

        // Audit logs
        public List<ContainerAuditLog> AuditLogs { get; set; } = new();
        
        // Filters
        [BindProperty(SupportsGet = true)]
        public string? FilterUsername { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? FilterAction { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? FilterItemNo { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? FilterStartDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? FilterEndDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 50;

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = _userService.GetCurrentUsername();
            
            // Check if user has access
            var canView = await _userService.CanViewAsync(CurrentUser);
            
            if (!canView)
            {
                _logger.LogWarning("❌ User {CurrentUser} attempted to access Reports without permission", CurrentUser);
                TempData["ErrorMessage"] = "You do not have permission to view reports.";
                return RedirectToPage("/Index");
            }

            UserRole = await _userService.GetUserRoleAsync(CurrentUser);
            HasAccess = true;

            _logger.LogInformation("✅ User {CurrentUser} accessed Reports page", CurrentUser);

            // Load audit logs with filters
            try
            {
                if (!string.IsNullOrWhiteSpace(FilterItemNo))
                {
                    // Search by specific Item No
                    AuditLogs = await _auditService.GetContainerHistoryAsync(FilterItemNo.Trim());
                }
                else
                {
                    // Apply other filters
                    AuditLogs = await _auditService.GetAuditLogsAsync(
                        username: FilterUsername,
                        action: FilterAction,
                        startDate: FilterStartDate,
                        endDate: FilterEndDate,
                        pageSize: PageSize
                    );
                }

                _logger.LogInformation("✅ Loaded {Count} audit log entries", AuditLogs.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error loading audit logs");
                TempData["ErrorMessage"] = "An error occurred while loading audit logs.";
                AuditLogs = new List<ContainerAuditLog>();
            }

            return Page();
        }
    }
}