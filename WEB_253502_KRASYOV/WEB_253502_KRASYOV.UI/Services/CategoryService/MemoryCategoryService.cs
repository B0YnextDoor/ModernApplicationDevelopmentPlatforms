using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.UI.Services.CategoryService
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			List<Category> deviceCategories =
		[
			new Category { Name = "Smartphone", NormalizedName = "phone" },
			new Category { Name = "Tablet", NormalizedName = "tablet" },
			new Category { Name = "Laptop", NormalizedName = "laptop" },
			new Category { Name = "Personal Computer", NormalizedName = "pc" },
			new Category { Name = "Game Console", NormalizedName = "console" },
			new Category { Name = "Headphones", NormalizedName = "earflaps" },
			new Category { Name = "Keyboard", NormalizedName = "board" },
			new Category { Name = "Computer Mouse", NormalizedName = "mouse" },
		];

			var result = ResponseData<List<Category>>.Success(deviceCategories);

			return Task.FromResult(result);
		}
	}
}
