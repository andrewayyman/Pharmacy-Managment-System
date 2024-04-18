namespace Pharmacy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // one to many with medicine
        public List<Medicine> Medicines { get; set; }

		// one to many with admin
		public int AdminId { get; set; } // FK

		public Admin Admin { get; set; } // Navigation property

    }
}
