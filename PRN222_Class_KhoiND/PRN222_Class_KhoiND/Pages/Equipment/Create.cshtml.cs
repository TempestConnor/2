using Class_KhoiND.Repositories.Models;
using Class_KhoiND.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PRN222_Class_KhoiND.Pages_Equipment
{
    public class CreateModel : PageModel
    {
        private readonly IMainService _mainService;
        private readonly SecondaryService _secondaryService;

        [BindProperty] public string? Name { get; set; }
        [BindProperty] public int? GroupId { get; set; }
        [BindProperty(Name = "Amount")] public string? AmountText { get; set; }

        public List<LookupGroup> Groups { get; set; } = new();

        public async Task OnGetAsync() => await LoadGroups();

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadGroups();


            //
            // ERROR MESSAGES
            //
            var name = Name?.Trim();
            if (string.IsNullOrEmpty(name))
                ModelState.AddModelError("Name", "Name is required");   // use the paper's wording
            else if (name.Length > 50)
                ModelState.AddModelError("Name", "Name must be at most 50 characters");

            if (GroupId == null || !Groups.Any(g => g.GroupId == GroupId))
                ModelState.AddModelError("GroupId", "Group is required");

            decimal amount = 0;
            var text = AmountText?.Trim();
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("Amount", "Amount is required");
            else if (!decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                         CultureInfo.InvariantCulture, out amount))
                ModelState.AddModelError("Amount", "Amount must be a number");
            else if (amount < 0 || amount > 1000)
                ModelState.AddModelError("Amount", "Amount must be between 0 and 1000");
            else if (decimal.Round(amount, 2) != amount)
                ModelState.AddModelError("Amount", "Amount can have at most 2 decimal places");
            //
            //
            //
           

            if (!ModelState.IsValid) return Page();

            await _mainService.CreateAsync(new Item
            {
                Name = name!,
                GroupId = GroupId!.Value,
                Amount = amount,
                EventDate = new DateOnly(2026, 3, 10),
                Status = "Pending"
            });

            return RedirectToPage("Index");
        }

        private async Task LoadGroups() =>
            Groups = (await _secondaryService.GetAllAsync()).OrderBy(g => g.Name).ToList();
    }
}
