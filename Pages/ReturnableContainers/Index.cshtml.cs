using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YmmcContainerTrackerApi.Data;
using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Pages_ReturnableContainers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<ReturnableContainers> ReturnableContainers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Guard against legacy rows with NULL Item_No to avoid SqlNullValueException
            ReturnableContainers = await _context.ReturnableContainers
                .AsNoTracking()
                .Where(rc => rc.ItemNo != null)
                .ToListAsync();
        }

        // GET: return edit-mode row
        public async Task<PartialViewResult> OnGetEditRowAsync(string id)
        {
            var item = await _context.ReturnableContainers.AsNoTracking().FirstOrDefaultAsync(x => x.ItemNo == id);
            if (item == null)
            {
                return Partial("_Row", new ReturnableContainersRowModel { Item = new ReturnableContainers { ItemNo = id }, IsEditing = false });
            }
            return Partial("_Row", new ReturnableContainersRowModel { Item = item, IsEditing = true });
        }

        // POST: save inline edit
        public async Task<PartialViewResult> OnPostSaveRowAsync([FromForm] ReturnableContainers item)
        {
            var existing = await _context.ReturnableContainers.FirstOrDefaultAsync(x => x.ItemNo == item.ItemNo);
            if (existing == null)
            {
                return Partial("_Row", new ReturnableContainersRowModel { Item = item, IsEditing = false });
            }

            var packing = (item.PackingCode ?? string.Empty).Trim();
            var prefix = (item.PrefixCode ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(packing) || string.IsNullOrWhiteSpace(prefix))
            {
                // return edit mode with same item
                return Partial("_Row", new ReturnableContainersRowModel { Item = item, IsEditing = true });
            }

            existing.PackingCode = packing;
            existing.PrefixCode = prefix;
            existing.ContainerNumber = (item.ContainerNumber ?? string.Empty).Trim();
            existing.OutsideLength = item.OutsideLength;
            existing.OutsideWidth = item.OutsideWidth;
            existing.OutsideHeight = item.OutsideHeight;
            existing.CollapsedHeight = item.CollapsedHeight;
            existing.Weight = item.Weight;
            existing.PackQuantity = item.PackQuantity;
            existing.AlternateId = (item.AlternateId ?? string.Empty).Trim();

            await _context.SaveChangesAsync();

            return Partial("_Row", new ReturnableContainersRowModel { Item = existing, IsEditing = false });
        }
    }
}
