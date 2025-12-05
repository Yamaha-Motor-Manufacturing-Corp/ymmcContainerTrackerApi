using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Models
{
 // Row view-model for inline editing partial
 public class ReturnableContainersRowModel
 {
 public ReturnableContainers Item { get; set; } = new();
 public bool IsEditing { get; set; }
 }
}
