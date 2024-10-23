using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Areas.Admin.Pages
{
	[Authorize(Policy = "admin")]
	public class EditModel : PageModel
    {
        private readonly IDeviceService _deviceService;
        private readonly ICategoryService _categoryService;

        public EditModel(IDeviceService deviceService, ICategoryService categoryService)
        {
            _deviceService = deviceService;
            _categoryService = categoryService;

            Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
        }

        [BindProperty]
        public Device Device { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public SelectList Categories { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _deviceService.GetDeviceByIdAsync(id.Value);
            if (!response.Successfull)
            {
                return NotFound();
            }
            Device = response.Data!;
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
                return Page();
            }

            try
            {
                await _deviceService.UpdateDeviceAsync(Device.Id, Device, Image);
            }
            catch (Exception)
            {
                if (!await DeviceExists(Device.Id))
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

        private async Task<bool> DeviceExists(int id)
        {
            var response = await _deviceService.GetDeviceByIdAsync(id);
            return response.Successfull;
        }
    }
}
