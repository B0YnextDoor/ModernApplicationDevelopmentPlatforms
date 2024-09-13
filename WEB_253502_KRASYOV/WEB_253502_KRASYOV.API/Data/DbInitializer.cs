using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.Domain.Entities;

namespace WEB_253502_KRASYOV.API.Data
{
	public class DbInitializer
	{
		public static async Task SeedData(WebApplication app)
		{
			var baseUrl = app.Configuration.GetSection("BaseUrl").Value;

			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			await context.Database.MigrateAsync();

			List<Category> Categories = [
				new Category { Name = "Smartphone", NormalizedName = "phone" },
				new Category { Name = "Tablet", NormalizedName = "tablet" },
				new Category { Name = "Laptop", NormalizedName = "laptop" },
				new Category { Name = "Personal Computer", NormalizedName = "pc" },
				new Category { Name = "Game Console", NormalizedName = "console" },
				new Category { Name = "Headphones", NormalizedName = "earflaps" },
				new Category { Name = "Keyboard", NormalizedName = "board" },
				new Category { Name = "Computer Mouse", NormalizedName = "mouse" },
			];

			List<Device> Devices = [
				new Device {
					Name = "Iphone 15",
					Description = "Basic Iphone.",
					Price = 2600,
					Category = Categories[0],
					CategoryId = 1,
					Image = $"{baseUrl}/images/iphone.jpg"
				},
				new Device {
					Name = "Ipad Pro 12.9 M2",
					Description = "Powerful Tablet.",
					Price = 4600,
					Category = Categories[1],
					CategoryId = 2,
					Image = $"{baseUrl}/images/ipad.jpg"
				},
				new Device {
					Name = "MacBook Air 15 M2",
					Description = "Style, Convenience & Performance.",
					Price = 5400,
					Category = Categories[2],
					CategoryId = 3,
					Image = $"{baseUrl}/images/laptop.jpg"
				},
				new Device {
					Name = "Hyper PC G1 PRO",
					Description = "A work computer.",
					Price = 5355,
					Category = Categories[3],
					CategoryId = 4,
					Image = $"{baseUrl}/images/pc.png"
				},
				new Device {
					Name = "PS 5",
					Description = "Balanced solution for gamers.",
					Price = 2400,
					Category = Categories[4],
					CategoryId = 5,
					Image = $"{baseUrl}/images/ps5.jpg"
				},
				new Device {
					Name = "AirPods Max",
					Description = "Deep, high quality sound.",
					Price = 2200,
					Category = Categories[5],
					CategoryId = 6,
					Image = $"{baseUrl}/images/airpods.jpg"
				},
				new Device {
					Name = "Logitech G915 GL Tactile",
					Description = "Wireless mechanical gaming keyboard.",
					Price = 940,
					Category = Categories[6],
					CategoryId = 7,
					Image = $"{baseUrl}/images/board.jpg"
				},
				new Device {
					Name = "Logitech Pro X Superlight",
					Description = "Full size gaming mouse for PC.",
					Price = 550,
					Category = Categories[7],
					CategoryId = 8,
					Image = $"{baseUrl}/images/mouse.jpg"
				},
				new Device {
					Name = "Samsung Galaxy S23",
					Description = "Basic Samsung.",
					Price = 2100,
					Category = Categories[0],
					CategoryId = 1,
					Image = $"{baseUrl}/images/samsung.jpg"
				},
				new Device {
					Name = "Xiaomi Pad 6",
					Description = "Not very powerful Tablet.",
					Price = 1700,
					Category = Categories[1],
					CategoryId = 2,
					Image = $"{baseUrl}/images/mipad.jpg"
				},
				new Device {
					Name = "Honor MagicBook X 14 Pro",
					Description = "Chinese style, Convenience & Performance.",
					Price = 3100,
					Category = Categories[2],
					CategoryId = 3,
					Image = $"{baseUrl}/images/honor.jpg"
				},
				new Device {
					Name = "Alienware area 51 r5",
					Description = "Another work computer.",
					Price = 2500,
					Category = Categories[3],
					CategoryId = 4,
					Image = $"{baseUrl}/images/alienware.jpg"
				},
				new Device {
					Name = "Xbox Series X",
					Description = "Balanced solution for gamers who loves Halo.",
					Price = 2200,
					Category = Categories[4],
					CategoryId = 5,
					Image = $"{baseUrl}/images/xbox.jpg"
				},
				new Device {
					Name = "Beats studio buds+",
					Description = "High quality sound & small size.",
					Price = 1300,
					Category = Categories[5],
					CategoryId = 6,
					Image = $"{baseUrl}/images/beats.jpg"
				},
				new Device {
					Name = "Razer Ornata V3 X",
					Description = "Wired mechanical gaming keyboard.",
					Price = 160,
					Category = Categories[6],
					CategoryId = 7,
					Image = $"{baseUrl}/images/razerboard.jpg"
				},
				new Device {
					Name = "Blody X5 Pro",
					Description = "Another gaming mouse for PC.",
					Price = 120,
					Category = Categories[7],
					CategoryId = 8,
					Image = $"{baseUrl}/images/blodymouse.jpg"
				},
			];

			await context.Categories.AddRangeAsync(Categories);
			await context.Devices.AddRangeAsync(Devices);

			await context.SaveChangesAsync();
		}
	}
}
