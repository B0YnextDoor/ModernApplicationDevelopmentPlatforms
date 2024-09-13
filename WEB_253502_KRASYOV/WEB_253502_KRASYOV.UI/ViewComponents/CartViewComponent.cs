using Microsoft.AspNetCore.Mvc;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.UI.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
		private readonly Cart _cart;
		public CartViewComponent(Cart cart) {
			_cart = cart;
		}

		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult<IViewComponentResult>(View(_cart));
		}
	}
}
