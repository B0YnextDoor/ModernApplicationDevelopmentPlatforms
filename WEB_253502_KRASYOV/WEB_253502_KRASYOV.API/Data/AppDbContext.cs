using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.Domain.Entities;

namespace WEB_253502_KRASYOV.API.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Device> Devices { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
