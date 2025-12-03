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
        private readonly YmmcContainerTrackerApi.Data.AppDbContext _context;

        public IndexModel(YmmcContainerTrackerApi.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<ReturnableContainers> ReturnableContainers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ReturnableContainers = await _context.ReturnableContainers.ToListAsync();
        }
    }
}
