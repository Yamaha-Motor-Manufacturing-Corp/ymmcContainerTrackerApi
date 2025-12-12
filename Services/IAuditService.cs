using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Services;

public interface IAuditService
{
  
    /// Log when a container is created
    Task LogCreateAsync(string itemNo, ReturnableContainers newContainer, string username, string? ipAddress = null);


    /// Log when a container is updated
    Task LogUpdateAsync(string itemNo, ReturnableContainers oldContainer, ReturnableContainers newContainer, string username, string? ipAddress = null);


    /// Log when a container is deleted
    Task LogDeleteAsync(string itemNo, ReturnableContainers deletedContainer, string username, string? ipAddress = null);


    /// Log when a container is viewed
    Task LogViewAsync(string itemNo, string username, string? ipAddress = null);


    /// Get audit logs for a specific container
    Task<List<ContainerAuditLog>> GetContainerHistoryAsync(string itemNo);


    /// Get all audit logs with optional filtering
    Task<List<ContainerAuditLog>> GetAuditLogsAsync(string? username = null, string? action = null, DateTime? startDate = null, DateTime? endDate = null, int pageSize = 100);


    /// Get recent activity (last N records)
    Task<List<ContainerAuditLog>> GetRecentActivityAsync(int count = 50);
}
