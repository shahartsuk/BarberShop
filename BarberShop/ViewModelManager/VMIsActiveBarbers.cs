using BarberShop.Models;

namespace BarberShop.ViewModelManager
{
    public class VMIsActiveBarbers
    {
        public VMIsActiveBarbers(List<User> Barbers) 
        {
            ActiveBarbers = Barbers.OfType<Barber>().ToList().FindAll(b => b.Active);
            NotActiveBarbers = Barbers.OfType<Barber>().ToList().FindAll(b => !b.Active);
            Users = Barbers.OfType<ClientUser>().ToList();
            //ActiveBarbers = Barbers.Cast<Barber>().ToList().FindAll(b => b.Active && b is Barber);
            //NotActiveBarbers = Barbers.Cast<Barber>().ToList().FindAll(b => !b.Active && b is Barber);
        }
        public List<Barber> ActiveBarbers { get; set; }
        public List<Barber> NotActiveBarbers { get; set; }
        public List<ClientUser> Users { get; set; }
    }
}
