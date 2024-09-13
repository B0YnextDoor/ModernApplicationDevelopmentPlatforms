using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.UI.Controllers;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;

namespace WEB_253502_KRASYOV.Tests
{
	public class ProductControllerTest
	{
		private readonly ICategoryService _categoryService;
		private readonly IDeviceService _deviceService;

		public ProductControllerTest()
		{
			_deviceService = Substitute.For<IDeviceService>();
			_categoryService = Substitute.For<ICategoryService>();
		}

		private List<Category> GetTestCategories()
		{
			return new List<Category>() {
				new Category() { Id = 1, Name="Name1", NormalizedName="name-1"},
				new Category() { Id = 2, Name="Name2", NormalizedName="name-2"}
			};
		}

		private List<Device> GetTestDevices()
		{
			return new List<Device>()
				{
					new Device() { Id = 1, Price=1032M, Description="device-1", Name="Device-1", CategoryId = 1},
					new Device() { Id = 2, Price=66M, Description="device-2", Name="Device-2", CategoryId = 2},
				};
		}

		[Fact]
		public void Index_GettingProductListFailed_ShouldReturn404()
		{
			_deviceService.GetDeviceListAsync(null).Returns(new ResponseData<ListModel<Device>>()
			{
				Successfull = false
			});

			_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
			{
				Successfull = true
			});

			var controllerContext = new ControllerContext();
			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Headers.Returns(new HeaderDictionary());
			controllerContext.HttpContext = httpContext;

			var controller = new Product(_deviceService, _categoryService)
			{
				ControllerContext = controllerContext
			};

			// Act
			var result = controller.Index(null).Result;

			// Assert
			Assert.IsType<NotFoundObjectResult>(result);
		}

		[Fact]
		public void Index_GettingCategoryListFailed_ShouldReturn404()
		{
			// Arrange
			_deviceService.GetDeviceListAsync(null).Returns(new ResponseData<ListModel<Device>>()
			{
				Successfull = true
			});

			_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
			{
				Successfull = false
			});

			var controllerContext = new ControllerContext();
			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Headers.Returns(new HeaderDictionary());
			controllerContext.HttpContext = httpContext;

			var controller = new Product(_deviceService, _categoryService)
			{
				ControllerContext = controllerContext
			};

			// Act
			var result = controller.Index(null).Result;

			// Assert
			Assert.IsType<NotFoundObjectResult>(result);
		}

		[Fact]
		public void Index_ViewData_Should_Contain_CategoryList()
		{
			// Arrange
			_deviceService.GetDeviceListAsync(null).Returns(new ResponseData<ListModel<Device>>()
			{
				Successfull = true,
				Data = new ListModel<Device>
				{
					Items = GetTestDevices()
				}
			});

			_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
			{
				Successfull = true,
				Data = GetTestCategories()
			});

			var controllerContext = new ControllerContext();
			var tempDataProvider = Substitute.For<ITempDataProvider>();
			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Headers.Returns(new HeaderDictionary());
			controllerContext.HttpContext = httpContext;

			var controller = new Product(_deviceService, _categoryService)
			{
				ControllerContext = controllerContext,
				TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
			};

			// Act
			var result = controller.Index(null).Result;

			// Assert
			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var categories = viewResult.ViewData["categories"] as List<Category>;

			Assert.NotNull(categories);
			Assert.NotEmpty(categories);
			Assert.Equal(GetTestCategories(), categories, new CategoryComparer());
		}

		[Theory]
		[InlineData(null)]
		[InlineData("name-2")]
		public void Index_ViewData_Should_Contain_CorrectCurrentCategory(string? category)
		{
			var arg = category is null ? "Все" : "Name2";
			// Arrange
			_deviceService.GetDeviceListAsync(category).Returns(new ResponseData<ListModel<Device>>()
			{
				Successfull = true,
				Data = new ListModel<Device>
				{
					Items = GetTestDevices()
				}
			});

			_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
			{
				Successfull = true,
				Data = GetTestCategories()
			});

			var controllerContext = new ControllerContext();
			var tempDataProvider = Substitute.For<ITempDataProvider>();
			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Headers.Returns(new HeaderDictionary());
			controllerContext.HttpContext = httpContext;

			var controller = new Product(_deviceService, _categoryService)
			{
				ControllerContext = controllerContext,
				TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
			};

			// Act
			var result = controller.Index(category).Result;

			// Assert
			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var currentCategory = viewResult.ViewData["current"].ToString();

			Assert.NotNull(currentCategory);
			Assert.NotEmpty(currentCategory);
			Assert.Equal(arg, currentCategory);
		}

		[Fact]
		public void Index_View_Should_Contain_ProductList()
		{
			// Arrange
			_deviceService.GetDeviceListAsync(null).Returns(new ResponseData<ListModel<Device>>()
			{
				Successfull = true,
				Data = new ListModel<Device>
				{
					Items = GetTestDevices()
				}
			});

			_categoryService.GetCategoryListAsync().Returns(new ResponseData<List<Category>>()
			{
				Successfull = true,
				Data = GetTestCategories()
			});

			var controllerContext = new ControllerContext();
			var tempDataProvider = Substitute.For<ITempDataProvider>();
			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Headers.Returns(new HeaderDictionary());
			controllerContext.HttpContext = httpContext;

			var controller = new Product(_deviceService, _categoryService)
			{
				ControllerContext = controllerContext,
				TempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider)
			};

			// Act
			var result = controller.Index(null).Result;

			// Assert
			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var productsList = Assert.IsType<ListModel<Device>>(viewResult.Model);

			Assert.NotNull(productsList);
			Assert.NotEmpty(productsList.Items);
			Assert.Equal(GetTestDevices(), productsList.Items, new DeviceComparer());
		}

	}

	public class CategoryComparer : IEqualityComparer<Category>
	{
		public bool Equals(Category? x, Category? y)
		{
			if (ReferenceEquals(x, y))
				return true;

			if (ReferenceEquals(y, null) || ReferenceEquals(x, null))
				return false;

			return x.Name == y.Name && x.NormalizedName == y.NormalizedName;
		}

		public int GetHashCode([DisallowNull] Category obj)
		{
			return obj.Id.GetHashCode() ^ obj.Name.GetHashCode() ^ obj.NormalizedName.GetHashCode();
		}
	}

	public class DeviceComparer : IEqualityComparer<Device>
	{
		public bool Equals(Device? x, Device? y)
		{
			if (ReferenceEquals(x, y))
				return true;

			if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
				return false;

			return x.CategoryId == y.CategoryId
				&& x.Description == y.Description
				&& x.Name == y.Name
				&& x.Price == y.Price
				&& x.Image == y.Image;
		}

		public int GetHashCode([DisallowNull] Device obj)
		{
			return obj.Id.GetHashCode()
				^ obj.Price.GetHashCode()
				^ obj.CategoryId.GetHashCode()
				^ obj.Description.GetHashCode()
				^ obj.Name.GetHashCode();
		}
	}
}
