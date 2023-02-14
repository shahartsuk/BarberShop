using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
	public class Entering
	{
		[Key]
		public int ID { get; set; }
		[Display(Name ="תאריך")]
		public DateTime Date { get; set; }
	}
}
