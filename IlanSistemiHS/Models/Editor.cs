namespace IlanSistemiHS.Models
{
	public class Editor : BaseModel
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public DateTime PublishDate { get; set; }
		public string ImageUrl { get; set; }
		public decimal Price { get; set; }
		public string Currency { get; set; }
	}
}