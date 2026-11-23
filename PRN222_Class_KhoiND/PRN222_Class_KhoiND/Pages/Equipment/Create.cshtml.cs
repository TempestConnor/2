using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Class_KhoiND.Repositories.Models;

namespace PRN222_Class_KhoiND.Pages_Equipment
{
    public class CreateModel : PageModel
    {
        private readonly Class_KhoiND.Repositories.Models.PRN222_PE01_2Context _context;

        public CreateModel(Class_KhoiND.Repositories.Models.PRN222_PE01_2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GroupId"] = new SelectList(_context.LookupGroups, "GroupId", "Name");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
