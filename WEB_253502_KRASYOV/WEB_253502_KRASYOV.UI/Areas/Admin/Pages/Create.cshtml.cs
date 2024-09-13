using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IDeviceService _deviceService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IDeviceService deviceService, ICategoryService categoryService)
        {
            _deviceService = deviceService;
            _categoryService = categoryService;

            Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Device Device { get; set; } = default!;

        public SelectList Categories { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Debug.WriteLine(Image);
            var response = await _deviceService.CreateDeviceAsync(Device, Image);

            if(!response.Successfull) return Page();

            return RedirectToPage("./Index");
        }
    }
}
