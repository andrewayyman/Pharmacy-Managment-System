namespace Pharmacy.Dto
{
	public class RequestDto
	{
		public int Id { get; set; }
		public RequestStatus Status { get; set; }

		public int PatientId { get; set; }
		public int AdminId { get; set; }
		public List<int > MedicineIds { get; set; }

	}
}
