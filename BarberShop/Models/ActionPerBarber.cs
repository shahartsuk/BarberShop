using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class ActionPerBarber
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="זמן פעולה בדקות")]
        public int ActionDuration { get; set; }
        [Display(Name ="מחיר")]
        public int Price { get; set; }
        [Display(Name ="חילוק העבודה לפי האחוזים")]
        public int PercentPerWork { get; set; }
        public Barber Barber { get; set; }
        public HairCutActions Action { get; set; }
    }
}
