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
    public class DetailsModel : PageModel
    {
        private readonly YmmcContainerTrackerApi.Data.AppDbContext _context;

        public DetailsModel(YmmcContainerTrackerApi.Data.AppDbContext context)
        {
            _context = context;
        }

        public ReturnableContainers ReturnableContainers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnablecontainers = await _context.ReturnableContainers.FirstOrDefaultAsync(m => m.ItemNo == id);
            if (returnablecontainers == null)
            {
                return NotFound();
            }
            else
            {
                ReturnableContainers = returnablecontainers;
            }
            return Page();
        }
    }
}
