using System.ComponentModel.DataAnnotations;

namespace YmmcContainerTrackerApi.Models;


/// Tracks all changes made to ReturnableContainers table
public class ContainerAuditLog
{
    [Key]
    public int Id { get; set; }


    /// The ItemNo of the container that was modified
    [Required]
    [StringLength(50)]
    public string ItemNo { get; set; } = string.Empty;


    /// Type of action: CREATE, UPDATE, DELETE, VIEW
    [Required]
    [StringLength(20)]
    public string Action { get; set; } = string.Empty;


    /// Username who performed the action
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

  
    /// User's role at the time of action (Admin, Editor, Viewer)
    [StringLength(20)]
    public string? UserRole { get; set; }

    /// When the action occurred
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

 
    /// JSON representation of values BEFORE the change (for UPDATE/DELETE)
    public string? OldValues { get; set; }

    
    /// JSON representation of values AFTER the change (for CREATE/UPDATE)
    public string? NewValues { get; set; }

   
    /// List of fields that were changed (for UPDATE)
    /// Example: "PackingCode, OutsideLength, Weight"
    [StringLength(500)]
    public string? ChangedFields { get; set; }

    /// IP address of the user
    [StringLength(45)]
    public string? IpAddress { get; set; }

    /// Browser/User Agent information
    [StringLength(500)]
    public string? UserAgent { get; set; }

    /// Optional notes or reason for change
    [StringLength(1000)]
    public string? Notes { get; set; }
}
