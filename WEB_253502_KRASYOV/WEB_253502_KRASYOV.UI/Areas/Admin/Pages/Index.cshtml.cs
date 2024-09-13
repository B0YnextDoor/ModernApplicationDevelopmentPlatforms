using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.UI.Extensions;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDeviceService _deviceService;

        public IndexModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [BindProperty]
        public List<Device> Items { get;set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNo = 1)
        {
            var response = await _deviceService.GetDeviceListAsync(null, pageNo);

            if (!response.Successfull) return RedirectToPage("/Error");

            Items = response.Data!.Items;
            TotalPages = response.Data.TotalPages;
            CurrentPage = pageNo;

            if(!Request.IsAjaxRequest()) return Page();

            return Partial("_ItemsDisplayPartial", new ListModel<Device>()
            {
                Items = Items,
                CurrentPage = CurrentPage,
                TotalPages = TotalPages,
            });

        }
    }
}
