using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Dto;


namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly PharmacyDbContext _context;
        public MedicinesController( PharmacyDbContext context )
        {
            _context = context;
        }
    #region ENDPOINTS
        // Endpoint //

    #region ViewMedicines
        // GetMedicines => api/Medicines
        [HttpGet]
        public async Task<IActionResult> ViewMedicines()
        {
            var Medicines = await _context.medicines.ToListAsync();
            return Ok(Medicines);
        }
        #endregion

    #region ViewMedicineById
        // GetMedicineById => api/Medicines/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var Medicine = await _context.medicines.FindAsync(id);
            if (Medicine == null)
                return NotFound($"No Medicine Found with Id{id}");
            return Ok(Medicine);
        }
        #endregion

    #region AddMedicine  // error
        // AddMedicine => api/Medicines
        [HttpPost]
        public async Task<IActionResult> AddMedicine(MedicineDto dto)
        {
            var isvalidCategory = await _context.categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!isvalidCategory)
            {
                return BadRequest($"Invalid Category No Category with Id {dto.CategoryId}");
            }
            var Medicine = new Medicine
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
            };

            await _context.medicines.AddAsync(Medicine);
            await _context.SaveChangesAsync();
            return Ok(Medicine);
        }
        #endregion

    #region UpdateMedicine
        // UpdateMedicine => api/Medicines/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, MedicineDto dto)
        {
            var Medicine = await _context.medicines.FindAsync(id);
            if (Medicine == null)
                return NotFound($"No Medicine Found with Id{id}");

            var isvalidCategory = await _context.categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!isvalidCategory)
            {
                return BadRequest($"Invalid Category No Category with Id {dto.CategoryId}");
            }

            Medicine.Name = dto.Name;
            Medicine.Description = dto.Description;
            Medicine.Price = dto.Price;
            Medicine.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(Medicine);
        }



        #endregion

    #region DeleteMedicine
        // DeleteMedicine => api/Medicines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var Medicine = await _context.medicines.FindAsync(id);
            if (Medicine == null)
                return NotFound($"No Medicine Found with Id{id}");

            _context.medicines.Remove(Medicine);
            await _context.SaveChangesAsync();
            return Ok(Medicine);
        }

    #endregion




        #endregion
    }
}
