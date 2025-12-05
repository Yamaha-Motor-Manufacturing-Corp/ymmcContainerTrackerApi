namespace YmmcContainerTrackerApi.Pages_ReturnableContainers
{
 using YmmcContainerTrackerApi.Models;

 // Standalone row view-model for inline editing partial
 public class RowModel
 {
 public ReturnableContainers Item { get; set; } = new();
 public bool IsEditing { get; set; }
 }
}
