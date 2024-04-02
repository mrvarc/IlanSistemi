using IlanSistemiHS.Models;

namespace IlanSistemiHS.ViewModels
{
	public class HomePageVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public DateTime PublishDate { get; set; }
		public string ImageUrl { get; set; }
		public decimal Price { get; set; }
		public string Currency { get; set; }
	}
}