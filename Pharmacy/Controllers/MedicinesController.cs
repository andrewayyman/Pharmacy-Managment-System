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

        #region ViewMedicines => api/Medicines
        [HttpGet]
        public async Task<IActionResult> ViewMedicines()
        {
            var Medicines = await _context.medicines.ToListAsync();
            return Ok(Medicines);
        }
        #endregion

        #region ViewMedicineById => api/Medicines/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var Medicine = await _context.medicines.FindAsync(id);

        // validate if the Medicine id is not found
        if ( Medicine == null)
                return NotFound($"No Medicine Found with Id{id}");

        return Ok(Medicine);
        }
        #endregion

        #region AddMedicine api/Medicines
        [HttpPost]
        public async Task<IActionResult> AddMedicine(MedicineDto dto)
        {
            // validate the entered category id
            var isvalidCategory = await _context.categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!isvalidCategory) return BadRequest($"Invalid Category No Category with Id {dto.CategoryId}");

            // validate the entered admin id
            var isvalidAdminId = await _context.patients.AnyAsync(a => a.AdminId == dto.AdminId);
            if ( !isvalidAdminId ) return BadRequest($"Invalid AdminID No AdminID with Id {dto.AdminId}");

        var Medicine = new Medicine
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                AdminId = dto.AdminId
            };

            await _context.medicines.AddAsync(Medicine);
            await _context.SaveChangesAsync();
            return Ok(Medicine);
        }
        #endregion

        #region UpdateMedicine api/Medicines/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, MedicineDto dto)
        {
            var Medicine = await _context.medicines.FindAsync(id);

            // va;idate if the Medicine id is not found
            if (Medicine == null)
                return NotFound($"No Medicine Found with Id{id}");

            // validate the entered category id
            var isvalidCategory = await _context.categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!isvalidCategory) return BadRequest($"Invalid Category No Category with Id {dto.CategoryId}");

            // validate the entered admin id 
            var isvalidAdminId = await _context.patients.AnyAsync(a => a.AdminId == dto.AdminId);
            if ( !isvalidAdminId ) return BadRequest($"Invalid AdminID No AdminID with Id {dto.AdminId}");

            Medicine.Name = dto.Name;
            Medicine.Description = dto.Description;
            Medicine.Price = dto.Price;
            Medicine.CategoryId = dto.CategoryId;
            Medicine.AdminId = dto.AdminId;

            await _context.SaveChangesAsync();
            return Ok(Medicine);
        }



        #endregion

        #region DeleteMedicine api/Medicines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var Medicine = await _context.medicines.FindAsync(id);

            // validate if the Medicine id is not found
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
