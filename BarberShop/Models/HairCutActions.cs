using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class HairCutActions
    {
        public HairCutActions() {
            PerBarbers = new List<ActionPerBarber>();
        }
        [Key]
        public int ID { get; set; }
        [Display(Name = "שם פעולה")]
        public string? Name { get; set; }
        [Display(Name ="תיאור תספורת")]
        public string? Description { get; set; }
        [Display(Name ="תמונה")]
        public byte[]? Image { get; set; }
        public IFormFile SetImage{ set { Image = new SetImg(value).MyImage;}}
        //רשימה של כל הספרים שעושים את הפעולה הספציפית
        public List<ActionPerBarber>? PerBarbers { get; set; }
        
    }
}
