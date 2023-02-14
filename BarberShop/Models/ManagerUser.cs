using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    //המנהל יורש את הקלאס של הספר כי יכול להיות שהמנהל הוא גם ספר
    public class ManagerUser : User
    {
        [Display(Name = "תאריך תחילת המינוי")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
