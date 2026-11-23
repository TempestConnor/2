using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Class_KhoiND.Repositories.Models;

namespace PRN222_Class_KhoiND.Pages_Equipment
{
    public class IndexModel : PageModel
    {
        private readonly Class_KhoiND.Repositories.Models.PRN222_PE01_2Context _context;

        public IndexModel(Class_KhoiND.Repositories.Models.PRN222_PE01_2Context context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Item = await _context.Items
                .Include(i => i.Group).ToListAsync();
        }
    }
}
