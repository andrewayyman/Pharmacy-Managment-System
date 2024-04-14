namespace Pharmacy.Models
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    };

    public class Request
    {
        public int Id { get; set; }  
        public RequestStatus Status  { get; set; }
        public DateTime Date { get; set; }

        // one to many with patient
        public int PatientId { get; set; }  // FK
        public Patient Patient { get; set; } // Navigation property

        // many to many with medicine
        public List<Medicine> Medicines { get; set; } //relation with medicine is many to many

        // one to many with admin
        public int AdminId { get; set; }  // FK
        public Admin Admin { get; set; } // Navigation property

    }
}
