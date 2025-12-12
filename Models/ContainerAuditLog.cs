using System.ComponentModel.DataAnnotations;

namespace YmmcContainerTrackerApi.Models;

/// <summary>
/// Tracks all changes made to ReturnableContainers table
/// </summary>
public class ContainerAuditLog
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The ItemNo of the container that was modified
    /// </summary>
    [Required]
    [StringLength(50)]
    public string ItemNo { get; set; } = string.Empty;

    /// <summary>
    /// Type of action: CREATE, UPDATE, DELETE, VIEW
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Username who performed the action
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User's role at the time of action (Admin, Editor, Viewer)
    /// </summary>
    [StringLength(20)]
    public string? UserRole { get; set; }

    /// <summary>
    /// When the action occurred
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// JSON representation of values BEFORE the change (for UPDATE/DELETE)
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// JSON representation of values AFTER the change (for CREATE/UPDATE)
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// List of fields that were changed (for UPDATE)
    /// Example: "PackingCode, OutsideLength, Weight"
    /// </summary>
    [StringLength(500)]
    public string? ChangedFields { get; set; }

    /// <summary>
    /// IP address of the user
    /// </summary>
    [StringLength(45)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Browser/User Agent information
    /// </summary>
    [StringLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Optional notes or reason for change
    /// </summary>
    [StringLength(1000)]
    public string? Notes { get; set; }
}
