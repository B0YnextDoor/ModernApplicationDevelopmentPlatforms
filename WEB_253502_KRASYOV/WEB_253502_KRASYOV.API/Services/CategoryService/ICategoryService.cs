using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.API.Services.CategoryService
{
	public interface ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync();
	}
}
