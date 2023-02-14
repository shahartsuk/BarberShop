using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModelsHome
{
	public class VMLogin
	{
		[Display(Name = "הכנס כתובת מייל")]
		[EmailAddress(ErrorMessage = "כתובת מייל אינה תקינה")]
		public string Email { get; set; }
		[Display(Name = "הכנס סיסמא")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string FeedBack { get; set; } = "התחבר";
		public string Color { get; set; } = "black";
	}
}
