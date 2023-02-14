using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Models
{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public ClientUser Client { get; set; }
        public ActionPerBarber HairCut { get; set; }

        //פונקציה המציגה לספר את שמות הלקוחות
        [NotMapped,Display(Name = "שם הלוקח")]
        public string ClientName { get
            {
                if (Client == null) return "פנוי";
                return Client.FullName;
            }
        }
        [NotMapped]
        public string ColorStatus { get
            {
                if (Client == null) return "red";
                return "green";
            } }
        [NotMapped,Display(Name = "פגישות")]
        public string AppoitmentStatus { get
            {
                if (Client == null) return "פנויה";
                return "תפוסה";
            } }
    }
}
