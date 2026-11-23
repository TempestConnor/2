using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Class_KhoiND.Repositories.Models;
using Class_KhoiND.Services;

namespace PRN222_Class_KhoiND.Pages_Equipment
{
    public class IndexModel : PageModel
    {
        private readonly IMainService _service;
        private readonly SecondaryService _secondaryService;

        public IndexModel(IMainService service, SecondaryService secondaryService)
        {
            _service = service;
            _secondaryService = secondaryService;
        }

        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }
        public List<LookupGroup> Groups { get; set; } = new();
        public List<Item> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Groups = (await _secondaryService.GetAllAsync()).OrderBy(g => g.Name).ToList();
            Items = await _service.SearchAsync(GroupId);

        }
    }
}
