namespace Pharmacy.Dto
{
    public class PatientDto
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int AdminId { get; set; }    // FK

    }
}
