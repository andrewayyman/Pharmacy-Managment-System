using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Dto;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PharmacyDbContext _context;
        public PatientsController( PharmacyDbContext context )
        {
            _context = context;
    
        }
        #region ENDPOINTS

        #region ViewPatients => api/Patients
        [HttpGet]
        public async Task<IActionResult> ViewPatients()
        {
            var Patients = await _context.patients.ToListAsync();
            return Ok(Patients);        
        }
        #endregion

        #region ViewPatientById => api/Patients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ViewPatientById(int id)
        {
            var Patient = await _context.patients.FindAsync(id);
            // validate if the patient id is not found
            if ( Patient == null )
                return NotFound($"No Patient Found with Id{id}");
            return Ok(Patient);
        }
        #endregion

        #region AddPatient => api/Patients
        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto dto)
        {
            // validate the entered AdminId
            var isvalidAdminId = await _context.patients.AnyAsync( a=>a.AdminId == dto.AdminId );
            if ( !isvalidAdminId ) return BadRequest($"Invalid AdminID No AdminID with Id {dto.AdminId}");

            var Patient = new Patient
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Address = dto.Address,
                Phone = dto.Phone,
                AdminId = dto.AdminId

            };

            await _context.patients.AddAsync(Patient);
            await _context.SaveChangesAsync();
            return Ok(Patient);
        }

        #endregion

        #region UpdatePatient => api/Patients
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdatePatient (int id, PatientDto dto)
        {
            var Patient = await _context.patients.FindAsync(id);

            //validate if the patient id is not found
            if(Patient == null) return NotFound($"No Medicine Found with Id{id}");

            //validate the entered AdminId
            var isvalidAdminId = await _context.patients.AnyAsync(a => a.AdminId == dto.AdminId);
            if ( !isvalidAdminId ) return BadRequest($"Invalid AdminID No AdminID with Id {dto.AdminId}");

            Patient.Name = dto.Name;
            Patient.Email = dto.Email;
            Patient.Password = dto.Password;
            Patient.Address = dto.Address;
            Patient.Phone = dto.Phone;
            Patient.AdminId = dto.AdminId;

            await _context.SaveChangesAsync();
            return Ok(Patient);

        }



        #endregion

        #region DeletePatient => api/Patinets
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient (int id)
        {
            var Pateint = await _context.patients.FindAsync(id);

            // validate if the patient id is not found
            if ( Pateint == null )
                return NotFound($"No Patient Found with Id{id}");

            _context.patients.Remove(Pateint);
            await _context.SaveChangesAsync();
            return Ok(Pateint);
        }
        #endregion






        #endregion

    }
}
