using System.Data.Entity;
using System.Text.RegularExpressions;

namespace BarberShop.Models
{
    public class DataLayer: DbContext
    {
        //כדי לדעת איזה יוזר מחובר כרגע
        public User CurrUser = new ClientUser { FirstName = "התחבר" }; 
        private static DataLayer _Instance;
        private DataLayer() : base("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Barber-Shop;Data Source=LAPTOP-GJS6JBCP\\SQLEXPRESS")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());
            if (Users.ToList().Count == 0) Seed();
        }
        public static DataLayer Instance { get { if (_Instance == null) { _Instance = new DataLayer(); } return _Instance; } }

        public void Seed()
        {
            User Manager = new ManagerUser
            {
                FirstName = "שחר",
                LastName = "צוק",
                Email = "shahar@gamil.com",
                Password = "1234",
                PhoneNumber = "0543223211",
                Date = DateTime.Now
            };
            Users.Add(Manager);
            User Barber = new Barber
            {
                FirstName = "בר",
                LastName = "רחמין",
                Email = "bar@gamil.com",
                Password = "2345",
                PhoneNumber = "0543432",
                Description = "ספר בסדר"
            };
            Users.Add(Barber);
            SaveChanges();
        }
        public List<Barber> getBarbersAllIncludes
        {

            get
            {
                HairCutActions.Include(h => h.PerBarbers).ToList();
                return Users.OfType<Barber>().Include(b => b.Actions).Include(b => b.Appointments).ToList();
            }
        }
        //public DbSet<Barber> Barbers { get; set; }
        public DbSet<HairCutActions> HairCutActions { get; set;}
        public DbSet<ActionPerBarber> ActionPerBarber { get; set;}
        // DbSet<ManagerUser> ManagerUser { get; set; }
        //public DbSet<ClientUser> ClientUser { get; set; }
        public DbSet<User> Users { get; set;}

        public DbSet<Appointment> Appointments { get; set;}
    }
}
