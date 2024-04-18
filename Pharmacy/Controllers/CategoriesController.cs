using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Dto;

namespace Pharmacy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly PharmacyDbContext _context;

		public CategoriesController(PharmacyDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var Categories = await _context.categories.Select(c => new CategoryDto
			{
				Name = c.Name,
				AdminId = c.AdminId,
			}).ToListAsync();
			return Ok(Categories);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var Category = await _context.categories.FindAsync(id);
			if (Category == null)
				return NotFound();
			var dto = new CategoryDto
			{
				Name = Category.Name,
				AdminId = Category.AdminId,

			};
			return Ok(dto);
		}
		[HttpGet("GetByAdminId")]
		public async Task<IActionResult> GetByAdminId(int adminid)
		{
			var Category = await _context.categories.Where(c => c.AdminId == adminid).Select(c => new CategoryDto
			{
				Name = c.Name,
				AdminId = c.AdminId,
			}).ToListAsync();
			return Ok(Category);

		}

		[HttpPost]
		public async Task<IActionResult> AddCategory(CategoryDto dto)
		{
			var isvalidadmin = await _context.admins.AnyAsync(a => a.Id == dto.AdminId);
			if (!isvalidadmin)
			{
				return BadRequest("invalid admin");
			}
			var category = new Category
			{
				Name = dto.Name,
				AdminId = dto.AdminId
			};

			await _context.categories.AddAsync(category);
			await _context.SaveChangesAsync();

			var responseData = new
			{
				CategoryId = category.CategoryId,
				Name = category.Name,
				AdminId = category.AdminId
			};

			return Ok(responseData);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var category = await _context.categories.FindAsync(id);
			if (category == null)
			
				return NotFound($"No Category Was Found With ID {id}");
			_context.Remove(category);
			_context.SaveChanges();
			return Ok(category);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id , CategoryDto dto)
		{


			var category = await _context.categories.FindAsync(id);
			if (category == null)

				return NotFound($"No Category Was Found With ID {id}");
			category.Name = dto.Name;
			category.AdminId = dto.AdminId;
			_context.SaveChanges();
			return Ok(category);

		}
	}
}
