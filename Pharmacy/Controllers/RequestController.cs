using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Dto;

namespace Pharmacy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RequestController : ControllerBase
	{
		private readonly PharmacyDbContext _context;

		public RequestController(PharmacyDbContext context)
		{
			_context = context;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRequestStatus(int id, [FromBody] RequestDto requestDto)
		{
			// Find the request by id
			var Request = await _context.requests.FindAsync(id);

			if (Request == null)

				return NotFound(); // Request not found


			// Update request status
			Request.Status = requestDto.Status;
			_context.SaveChanges();
			return Ok(Request);
		}
	}
}
