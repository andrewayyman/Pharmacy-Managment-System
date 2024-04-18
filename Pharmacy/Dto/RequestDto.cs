namespace Pharmacy.Dto
{
	public class RequestDto
	{
		public int Id { get; set; }
		public RequestStatus Status { get; set; }
		public DateTime Date { get; set; }

		
		public int PatientId { get; set; }
		public int AdminId { get; set; }  

	}
}
