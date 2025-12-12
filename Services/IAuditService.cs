using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Services;

public interface IAuditService
{
    /// <summary>
    /// Log when a container is created
    /// </summary>
    Task LogCreateAsync(string itemNo, ReturnableContainers newContainer, string username, string? ipAddress = null);

    /// <summary>
    /// Log when a container is updated
    /// </summary>
    Task LogUpdateAsync(string itemNo, ReturnableContainers oldContainer, ReturnableContainers newContainer, string username, string? ipAddress = null);

    /// <summary>
    /// Log when a container is deleted
    /// </summary>
    Task LogDeleteAsync(string itemNo, ReturnableContainers deletedContainer, string username, string? ipAddress = null);

    /// <summary>
    /// Log when a container is viewed
    /// </summary>
    Task LogViewAsync(string itemNo, string username, string? ipAddress = null);

    /// <summary>
    /// Get audit logs for a specific container
    /// </summary>
    Task<List<ContainerAuditLog>> GetContainerHistoryAsync(string itemNo);

    /// <summary>
    /// Get all audit logs with optional filtering
    /// </summary>
    Task<List<ContainerAuditLog>> GetAuditLogsAsync(string? username = null, string? action = null, DateTime? startDate = null, DateTime? endDate = null, int pageSize = 100);

    /// <summary>
    /// Get recent activity (last N records)
    /// </summary>
    Task<List<ContainerAuditLog>> GetRecentActivityAsync(int count = 50);
}
