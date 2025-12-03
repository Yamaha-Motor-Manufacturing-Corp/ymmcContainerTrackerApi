using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YmmcContainerTrackerApi.Data;
using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Pages_ReturnableContainers
{
    public class EditModel : PageModel
    {
        private readonly YmmcContainerTrackerApi.Data.AppDbContext _context;

        public EditModel(YmmcContainerTrackerApi.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReturnableContainers ReturnableContainers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnablecontainers =  await _context.ReturnableContainers.FirstOrDefaultAsync(m => m.ItemNo == id);
            if (returnablecontainers == null)
            {
                return NotFound();
            }
            ReturnableContainers = returnablecontainers;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ReturnableContainers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnableContainersExists(ReturnableContainers.ItemNo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReturnableContainersExists(string id)
        {
            return _context.ReturnableContainers.Any(e => e.ItemNo == id);
        }
    }
}
