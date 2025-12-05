using System.Text.RegularExpressions;
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
    public class CreateModel : PageModel
    {
        private readonly YmmcContainerTrackerApi.Data.AppDbContext _context;

        public CreateModel(YmmcContainerTrackerApi.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReturnableContainers ReturnableContainers { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Normalize key fields before validation
            string Normalize(string? s)
            {
                var trimmed = (s ?? string.Empty).Trim();
                if (trimmed.Length ==0) return string.Empty;
                // collapse internal whitespace to single space
                trimmed = Regex.Replace(trimmed, "\\s+", " ");
                // uppercase for consistency
                return trimmed.ToUpperInvariant();
            }

            ReturnableContainers.ItemNo = Normalize(ReturnableContainers.ItemNo);
            ReturnableContainers.PackingCode = Normalize(ReturnableContainers.PackingCode);
            ReturnableContainers.PrefixCode = Normalize(ReturnableContainers.PrefixCode);

            if (string.IsNullOrWhiteSpace(ReturnableContainers.ItemNo))
                ModelState.AddModelError("ReturnableContainers.ItemNo", "Please add the column [Item_No].");
            if (string.IsNullOrWhiteSpace(ReturnableContainers.PackingCode))
                ModelState.AddModelError("ReturnableContainers.PackingCode", "Please add the column [Packing_Code].");
            if (string.IsNullOrWhiteSpace(ReturnableContainers.PrefixCode))
                ModelState.AddModelError("ReturnableContainers.PrefixCode", "Please add the column [Prefix_Code].");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for duplicates (by normalized ItemNo)
            var exists = await _context.ReturnableContainers
                .AsNoTracking()
                .AnyAsync(rc => rc.ItemNo == ReturnableContainers.ItemNo);

            if (exists)
            {
                ModelState.AddModelError("ReturnableContainers.ItemNo", "This Item No already exists.");
                return Page();
            }

            // Persist new entity
            _context.ReturnableContainers.Add(ReturnableContainers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
