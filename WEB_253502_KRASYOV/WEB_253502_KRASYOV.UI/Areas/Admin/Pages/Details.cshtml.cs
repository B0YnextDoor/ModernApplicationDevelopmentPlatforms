﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IDeviceService _deviceService;
        private readonly ICategoryService _categoryService;

        public DetailsModel(IDeviceService deviceService, ICategoryService categoryService)
        {
            _deviceService = deviceService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Device Device { get; set; } = default!;

        [BindProperty]
        public string CategoryName { get; set; } = string.Empty;

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

            var categoryResponse = await _categoryService.GetCategoryListAsync();
            if (!categoryResponse.Successfull)
            {
                return NotFound();
            }
            CategoryName = categoryResponse.Data.FirstOrDefault(c => c.Id == Device.CategoryId).Name;

            return Page();
        }
    }
}
