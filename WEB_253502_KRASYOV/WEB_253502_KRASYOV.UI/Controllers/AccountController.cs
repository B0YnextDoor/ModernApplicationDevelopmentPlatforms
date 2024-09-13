using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WEB_253502_KRASYOV.UI.Models;
using WEB_253502_KRASYOV.UI.Services.Authentication;

namespace WEB_253502_KRASYOV.UI.Controllers
{
	public class Account : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel user, 
                                            [FromServices] IAuthService authService)
        {
            if (!ModelState.IsValid) return View(user);

            if (user == null)
                return BadRequest();

            var result = await authService.RegisterUserAsync(user.Email, user.Password, user.Avatar);

            if (result.Result) return Redirect(Url.Action("Index", "Home"));

            return BadRequest(result.ErrorMessage);
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, 
                new AuthenticationProperties {
                RedirectUri = Url.Action("Index", "Home")
            });
        }

        [HttpPost]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, 
                new AuthenticationProperties {
                RedirectUri = Url.Action("Index", "Home")
            });
        }
    }
}
