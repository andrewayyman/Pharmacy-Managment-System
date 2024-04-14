
namespace Pharmacy.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        // one to many with admin
        public int AdminId { get; set; }    // FK
        public Admin Admin { get; set; }    // Navigation property

        // one to many with request
        public List<Request> Requests { get; set; }    // Navigation property

        // many to many with Medicine
        public List<Medicine> Medicines { get; set; }    // Navigation property , filter and view all medicines
        


    }
}
