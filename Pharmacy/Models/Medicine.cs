namespace Pharmacy
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // one to many with category
        public int CategoryId { get; set; } // FK
        public Category Category { get; set; } // Navigation property

        // many to many with request
        public List<Request> Requests { get; set; } 

        // one to many with admin
        public int AdminId { get; set; } // FK
        public Admin Admin { get; set; } // Navigation property

        // many to many with patient
        public List<Patient> Patients { get; set; } // Navigation property

       

    }
}
