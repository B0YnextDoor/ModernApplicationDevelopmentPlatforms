using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WEB_253502_KRASYOV.UI.Models;

namespace WEB_253502_KRASYOV.UI.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
			ViewData["lab"] = "Лабораторная работа 1";

			var items = new List<ListDemo> {
				new() { Id = 1, Name = "Item 1" },
				new() { Id = 2, Name = "Item 2" },
				new() { Id = 3, Name = "Item 3" },
				new() { Id = 4, Name = "Item 4" },
				new() { Id = 5, Name = "Item 5" }
		    };

			return View(new SelectList(items, "Id", "Name"));
        }

		[HttpPost]
		public IActionResult Form(string? selectedItem, string? check1, string? check2, string radios, string? login, string? password)
		{
			Debug.WriteLine($"Selected Item: {(selectedItem is null ? "none" : selectedItem)}\nCheckbox1: {(check1 is null ? "off" : check1)}\n" +
				$"Checkbox2: {(check2 is null ? "off" : check2)}\nRadios: {radios}\nLogin: {(login is null ? "none" : login)}\n" +
				$"Password: {(password is null ? "none" : password)}");
			return RedirectToAction("Index", "Home");
		}
	}
}
