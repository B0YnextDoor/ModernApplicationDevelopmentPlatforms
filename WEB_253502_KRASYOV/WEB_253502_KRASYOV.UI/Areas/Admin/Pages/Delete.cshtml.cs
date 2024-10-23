using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Areas.Admin.Pages
{
	[Authorize(Policy = "admin")]
	public class DeleteModel : PageModel
    {
        private readonly IDeviceService _deviceService;
        private readonly ICategoryService _categoryService;

        public DeleteModel(IDeviceService deviceService, ICategoryService categoryService)
        {
           _deviceService = deviceService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Device Device { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _deviceService.DeleteDeviceAsync(id.Value);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            

            return RedirectToPage("./Index");
        }
    }
}
