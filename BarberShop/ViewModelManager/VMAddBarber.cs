using BarberShop.Models;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModelManager
{
    public class VMAddBarber
    {
        public VMAddBarber() { Barber = new Barber();
            Users = new List<User>();
            //HairCut = new List<HairCutActions>();
            //addAction = new ActionPerBarber();
        }
        public Barber Barber { get; set; }

        [Display(Name = "הוספת תמונה")]
        public IFormFile File { get; set; }
        public List<User> Users { get; set; }

        //public List<HairCutActions> HairCut { get; set; }
        //public ActionPerBarber addAction { get; set; }
    }
}
