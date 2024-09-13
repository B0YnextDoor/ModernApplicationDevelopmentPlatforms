extern alias ApiTypes;

using ApiTypes::WEB_253502_KRASYOV.API.Data;
using ApiTypes::WEB_253502_KRASYOV.API.Services.DeviceService;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data.Common;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.Tests
{
	public class ProductServiceTest : IDisposable
	{
		private readonly DbConnection _connection;
		private readonly DbContextOptions<AppDbContext> _contextOptions;
		public ProductServiceTest()
		{
			_connection = new SqliteConnection("Filename=:memory:");
			_connection.Open();

			_contextOptions = new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlite(_connection)
				.Options;

			using var context = new AppDbContext(_contextOptions);

			context.Database.EnsureCreated();

			context.Categories.AddRange(
				new Category { Id = 1, Name = "Name1", NormalizedName = "name-1" },
				new Category { Id = 2, Name = "Name2", NormalizedName = "name-2" },
				new Category { Id = 3, Name = "Name3", NormalizedName = "name-3" });

			context.Devices.AddRange(
				new Device() { Id = 1, Name = "Name1", Description = "name-1", CategoryId = 1, Price = 1},
				new Device() { Id = 2, Name = "Name2", Description = "name-2", CategoryId = 1, Price = 2 },
				new Device() { Id = 3, Name = "Name3", Description = "name-3", CategoryId = 2, Price = 3 },
				new Device() { Id = 4, Name = "Name4", Description = "name-4", CategoryId = 2, Price = 4 },
				new Device() { Id = 5, Name = "Name5", Description = "name-5", CategoryId = 3, Price = 5 },
				new Device() { Id = 6, Name = "Name6", Description = "name-6", CategoryId = 3, Price = 6},
				new Device() { Id = 7, Name = "Name7", Description = "name-7", CategoryId = 1, Price = 7 },
				new Device() { Id = 8, Name = "Name8", Description = "name-8", CategoryId = 2, Price = 8 },
				new Device() { Id = 9, Name = "Name9", Description = "name-9", CategoryId = 3, Price = 9 });

			context.SaveChanges();
		}

		AppDbContext CreateContext() => new AppDbContext(_contextOptions);
		public void Dispose() => _connection.Close();

		[Fact]
		public void ServiceReturnsFirstPageOfThreeItems()
		{
			using var context = CreateContext();
			var service = new DeviceService(context);
			var result = service.GetDeviceListAsync(null).Result;
			Assert.IsType<ResponseData<ListModel<Device>>>(result);
			Assert.True(result.Successfull);
			Assert.Equal(1, result.Data.CurrentPage);
			Assert.Equal(3, result.Data.Items.Count);
			Assert.Equal(3, result.Data.TotalPages);
			Assert.Equal(context.Devices.First(), result.Data.Items[0]);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(3)]
		public void Handle_ValidReuqest_ShouldCorrectlyChooseGivenPage(int pageNo)
		{
			// Arrange
			using var context = CreateContext();
			var service = new DeviceService(context);

			// Act
			var result = service.GetDeviceListAsync(null, pageNo: pageNo).Result;

			// Assert
			Assert.IsType<ResponseData<ListModel<Device>>>(result);
			Assert.True(result.Successfull);
			Assert.Equal(pageNo, result.Data.CurrentPage);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("name-1")]
		[InlineData("name-3")]
		public void Handle_ValidRequest_ShouldCorrectlyFilterByCategory(string? category)
		{
			// Arrange
			using var context = CreateContext();
			var service = new DeviceService(context);

			// Act
			var result = service.GetDeviceListAsync(category).Result;

			// Assert
			Assert.IsType<ResponseData<ListModel<Device>>>(result);
			Assert.True(result.Successfull);
			Assert.Equal(3, result.Data.Items.Count);
		}

		[Theory]
		[InlineData(30)]
		[InlineData(100)]
		public void Handle_SetPageSizeGreaterThanMaximum_ShouldNotAllowSet(int pageSize)
		{
			// Arrange
			using var context = CreateContext();
			var service = new DeviceService(context);

			// Act
			var result = service.GetDeviceListAsync(null, pageSize: pageSize).Result;

			// Assert
			Assert.IsType<ResponseData<ListModel<Device>>>(result);
			Assert.True(result.Successfull);
			Assert.True((int)Math.Ceiling(result.Data.Items.Count / (double)result.Data.TotalPages) != pageSize);
		}

		[Theory]
		[InlineData(4)]
		[InlineData(int.MaxValue)]
		public void Handle_PageNoGreaterThanMaximumRequest_ReturnsSuccesfullIsFalse(int pageNo)
		{
			// Arrange
			using var context = CreateContext();
			var service = new DeviceService(context);

			// Act
			var result = service.GetDeviceListAsync(null, pageNo: pageNo).Result;

			// Assert
			Assert.IsType<ResponseData<ListModel<Device>>>(result);
			Assert.False(result.Successfull);
		}
	}
}
