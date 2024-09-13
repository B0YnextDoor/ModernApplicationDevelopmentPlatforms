using Microsoft.AspNetCore.Mvc;
using WEB_253502_KRASYOV.UI.Extensions;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.UI.Controllers
{
	[Route("Catalog")]
	public class Product : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IDeviceService _deviceService;
		public Product(IDeviceService deviceService, ICategoryService categoryService)
		{
			_categoryService = categoryService;
			_deviceService = deviceService;
		}

		[HttpGet("{category?}")]
		public async Task<IActionResult> Index(string? category, int pageNo = 1)
		{
			var deviceResponse = await _deviceService.GetDeviceListAsync(category, pageNo);

			if (!deviceResponse.Successfull) return NotFound(deviceResponse.ErrorMessage);

			var categories = await _categoryService.GetCategoryListAsync();

			if(!categories.Successfull || categories.Data is null) return NotFound(categories.ErrorMessage);

			ViewData["categories"] = categories.Data;

			ViewData["current"] = category is null ? "Все" : 
				categories.Data.Find(c => c.NormalizedName.Equals(category))?.Name;

			if (!Request.IsAjaxRequest()) return View(deviceResponse.Data);

            return PartialView("_ItemsDisplayPartial", deviceResponse.Data!);
		}

	}
}
