using Microsoft.AspNetCore.Mvc;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.UI.Services.CategoryService;

namespace WEB_253502_KRASYOV.UI.Services.DeviceService
{
	public class MemoryDeviceService : IDeviceService
	{
		List<Device> _devices;
		List<Category> _categories;
		IConfiguration _config;
		public MemoryDeviceService([FromServices] IConfiguration config, ICategoryService categoryService)
		{
			_config = config;
			_categories = categoryService.GetCategoryListAsync().Result.Data;
			SetupData();
		}

		public Task<ResponseData<ListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1)
		{
			var itemsPerPage = _config.GetValue<int>("ItemsPerPage");
			var devices = _devices.Where(d => categoryNormalizedName is null || d.Category.NormalizedName.Equals(categoryNormalizedName))
				.ToList();
			int totalPages = (int)Math.Ceiling((double)devices.Count / itemsPerPage);
			var pagedItems = new ListModel<Device>
			{
				Items = devices.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
				CurrentPage = pageNo,
				TotalPages = totalPages,
			};
			var result = ResponseData<ListModel<Device>>.Success(pagedItems);

			return Task.FromResult(result);

		}

		public Task<ResponseData<Device>> CreateDeviceAsync(Device product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		public Task DeleteDeviceAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
		{
			throw new NotImplementedException();
		}


		public Task UpdateDeviceAsync(int id, Device product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		private void SetupData() { 
			_devices = [
				new Device {
					Id = 1, 
					Name = "Iphone 15", 
					Description = "Basic Iphone.", 
					Price = 2600,
					Category = _categories.Find(c => c.NormalizedName.Equals("phone")),
					CategoryId = 1,
					Image = "images/iphone.jpg"
				},
				new Device {
					Id = 2,
					Name = "Ipad Pro 12.9 M2",
					Description = "Powerful Tablet.",
					Price = 4600,
					Category = _categories.Find(c => c.NormalizedName.Equals("tablet")),
					CategoryId = 2,
					Image = "images/ipad.jpg"
				},
				new Device {
					Id = 3,
					Name = "MacBook Air 15 M2",
					Description = "Style, Convenience & Performance.",
					Price = 5400,
					Category = _categories.Find(c => c.NormalizedName.Equals("laptop")),
					CategoryId = 3,
					Image = "images/laptop.jpg"
				},
				new Device {
					Id = 4,
					Name = "Hyper PC G1 PRO",
					Description = "A work computer.",
					Price = 5355,
					Category = _categories.Find(c => c.NormalizedName.Equals("pc")),
					CategoryId = 4,
					Image = "images/pc.png"
				},
				new Device {
					Id = 5,
					Name = "PS 5",
					Description = "Balanced solution for gamers.",
					Price = 2400,
					Category = _categories.Find(c => c.NormalizedName.Equals("console")),
					CategoryId = 5,
					Image = "images/ps5.jpg"
				},
				new Device {
					Id = 6,
					Name = "AirPods Max",
					Description = "Deep, high quality sound.",
					Price = 2200,
					Category = _categories.Find(c => c.NormalizedName.Equals("earflaps")),
					CategoryId = 6,
					Image = "images/airpods.jpg"
				},
				new Device {
					Id = 7,
					Name = "Logitech G915 GL Tactile",
					Description = "Wireless mechanical gaming keyboard.",
					Price = 940,
					Category = _categories.Find(c => c.NormalizedName.Equals("board")),
					CategoryId = 7,
					Image = "images/board.jpg"
				},
				new Device {
					Id = 8,
					Name = "Logitech Pro X Superlight",
					Description = "Full size gaming mouse for PC.",
					Price = 550,
					Category = _categories.Find(c => c.NormalizedName.Equals("mouse")),
					CategoryId = 8,
					Image = "images/mouse.jpg"
				},
				new Device {
					Id = 9,
					Name = "Samsung Galaxy S23",
					Description = "Basic Samsung.",
					Price = 2100,
					Category = _categories.Find(c => c.NormalizedName.Equals("phone")),
					CategoryId = 1,
					Image = "images/samsung.jpg"
				},
				new Device {
					Id = 10,
					Name = "Xiaomi Pad 6",
					Description = "Not very powerful Tablet.",
					Price = 1700,
					Category = _categories.Find(c => c.NormalizedName.Equals("tablet")),
					CategoryId = 2,
					Image = "images/mipad.jpg"
				},
				new Device {
					Id = 11,
					Name = "Honor MagicBook X 14 Pro",
					Description = "Chinese style, Convenience & Performance.",
					Price = 3100,
					Category = _categories.Find(c => c.NormalizedName.Equals("laptop")),
					CategoryId = 3,
					Image = "images/honor.jpg"
				},
				new Device {
					Id = 12,
					Name = "Alienware area 51 r5",
					Description = "Another work computer.",
					Price = 2500,
					Category = _categories.Find(c => c.NormalizedName.Equals("pc")),
					CategoryId = 4,
					Image = "images/alienware.jpg"
				},
				new Device {
					Id = 13,
					Name = "Xbox Series X",
					Description = "Balanced solution for gamers who loves Halo.",
					Price = 2200,
					Category = _categories.Find(c => c.NormalizedName.Equals("console")),
					CategoryId = 5,
					Image = "images/xbox.jpg"
				},
				new Device {
					Id = 14,
					Name = "Beats studio buds+",
					Description = "High quality sound & small size.",
					Price = 1300,
					Category = _categories.Find(c => c.NormalizedName.Equals("earflaps")),
					CategoryId = 6,
					Image = "images/beats.jpg"
				},
				new Device {
					Id = 15,
					Name = "Razer Ornata V3 X",
					Description = "Wired mechanical gaming keyboard.",
					Price = 160,
					Category = _categories.Find(c => c.NormalizedName.Equals("board")),
					CategoryId = 7,
					Image = "images/razerboard.jpg"
				},
				new Device {
					Id = 16,
					Name = "Blody X5 Pro",
					Description = "Another gaming mouse for PC.",
					Price = 120,
					Category = _categories.Find(c => c.NormalizedName.Equals("mouse")),
					CategoryId = 8,
					Image = "images/blodymouse.jpg"
				},
				];
		}
	}
}
