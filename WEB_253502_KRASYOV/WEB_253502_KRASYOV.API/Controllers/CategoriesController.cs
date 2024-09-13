using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.API.Data;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.API.Services.CategoryService;

namespace WEB_253502_KRASYOV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
           _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Category>>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategoryListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            throw new NotImplementedException();

		}

		// POST: api/Categories
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
			throw new NotImplementedException();

		}

		// DELETE: api/Categories/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
			throw new NotImplementedException();
		}

		private bool CategoryExists(int id)
        {
			throw new NotImplementedException();
		}
	}
}
