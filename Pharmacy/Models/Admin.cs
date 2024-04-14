namespace Pharmacy.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public List<Patient> Patients { get; set; }  
        public List<Request> Requests { get; set; }  
        public List<Medicine> Medicines { get; set; }  
        public List<Category> Categories { get; set; }  


    }
}
