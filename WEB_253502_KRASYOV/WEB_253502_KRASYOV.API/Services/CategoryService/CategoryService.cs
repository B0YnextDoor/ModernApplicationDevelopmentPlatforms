using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.API.Data;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.API.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly AppDbContext _context;
		public CategoryService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			var categories = await _context.Categories.ToListAsync();
			if (categories.Count == 0 || categories is null)
			{
				return ResponseData<List<Category>>.Error("No categories in db");
			}

			return ResponseData<List<Category>>.Success(categories);
		}
	}
}
