namespace Pharmacy.Dto
{
    public class MedicineDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // FK
        public int AdminId { get; set; } // FK



    }
}
