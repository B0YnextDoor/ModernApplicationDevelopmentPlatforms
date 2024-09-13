using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253502_KRASYOV.UI.Services.DeviceService;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.UI.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly IDeviceService _deviceService;
		private readonly Cart _cart;

		public CartController(IDeviceService deviceService, Cart cart)
		{
			_deviceService = deviceService;
			_cart = cart;
		}
		public IActionResult Index()
		{
			return View(_cart);
		}

		[Route("[controller]/add/{id:int}")]
		public async Task<IActionResult> Add(int id, string returnUrl)
		{
			var response = await _deviceService.GetDeviceByIdAsync(id);

			if(response.Successfull) _cart.AddToCart(response.Data);

			return Redirect(returnUrl);
		}

		[Route("[controller]/remove/{id:int}")]
		public async Task<IActionResult> Remove(int id, string returnUrl)
		{
			var response = await _deviceService.GetDeviceByIdAsync(id);

			if (response.Successfull) _cart.RemoveItems(id);

			return Redirect(returnUrl);
		}
	}
}
