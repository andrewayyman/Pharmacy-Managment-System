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


		#region ENDPOINTS

		#region Get Pending Requests
		[HttpGet("requests/pending")]
		public async Task<IActionResult> GetPendingRequests()
		{
			var pendingRequests = await _context.requests
				.Where(r => r.Status == RequestStatus.Pending)
				.Select(r => new RequestDto
				{
					Id = r.Id,
					Status = r.Status,
					PatientId = r.PatientId,
					AdminId = r.AdminId,
					MedicineIds = r.Medicines.Select(m => m.Id).ToList()
				})
				.ToListAsync();

			return Ok(pendingRequests);
		}
		#endregion

		#region Get All Requests 

		[HttpGet("requests")]
		public async Task<IActionResult> GetAllRequests()
		{
			var requests = await _context.requests
				.Select(r => new RequestDto
				{
					Id = r.Id,
					Status = r.Status,
					PatientId = r.PatientId,
					AdminId = r.AdminId,
					MedicineIds = r.Medicines.Select(m => m.Id).ToList()
				})
				.ToListAsync();

			return Ok(requests);
		}
		#endregion

		#region Get Requests By Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			// Retrieve the request by its ID, including associated medicines
			var request = await _context.requests
				.Include(r => r.Medicines)
				.FirstOrDefaultAsync(r => r.Id == id);

			// If request is not found, return 404 Not Found
			if (request == null)
			{
				return NotFound();
			}

			// Map the request entity to a DTO
			var requestDto = new RequestDto
			{
				Id = request.Id,
				Status = request.Status,
				AdminId = request.AdminId,
				PatientId = request.PatientId,
				MedicineIds = request.Medicines.Select(m => m.Id).ToList()
			};

			return Ok(requestDto);
		}
		#endregion

		#region Post A Request
		[HttpPost]
		public async Task<IActionResult> CreateRequest([FromBody] RequestDto requestDto)
		{
			// Create a new request entity
			var request = new Request
			{
				PatientId = requestDto.PatientId,
			};

			// Retrieve medicines based on the IDs provided in the DTO
			var medicines = await _context.medicines
				.Where(m => requestDto.MedicineIds.Contains(m.Id))
				.ToListAsync();

			// Add medicines to the request
			// Check if Medicines property is null
			if (request.Medicines == null)
			{
				// If it's null, initialize it with an empty list
				request.Medicines = new List<Medicine>();
			}

			// Now you can safely add medicines to request.Medicines
			request.Medicines.AddRange(medicines);


			// Add the request to the context and save changes
			_context.requests.Add(request);
			await _context.SaveChangesAsync();

			// Return the created request DTO with the newly generated ID
			var createdRequestDto = new RequestDto
			{
				PatientId = request.PatientId,
				MedicineIds = request.Medicines.Select(m => m.Id).ToList()
			};

			return CreatedAtAction(nameof(GetById), new { id = createdRequestDto.Id }, createdRequestDto);
		}
		#endregion

		#region Update Requests Statue

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
		#endregion

		#endregion

	}
}
