using BarberShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BarberShop.ViewModelsHome;

namespace BarberShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
		public IActionResult LogIn(int? id)
        {
            if(id == null) DataLayer.Instance.CurrUser = new ClientUser { FirstName = "התחבר" };
            return View(new VMLogin());
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult LogIn(VMLogin login) 
        {
            User user = DataLayer.Instance.Users.FirstOrDefault(u=>u.Email== login.Email && u.Password == login.Password);
            if (user == null)
            {
                login.FeedBack = "פרטים שגויים";
                login.Color = "red";
                return View(login);
            }
            DataLayer.Instance.CurrUser = user;
            if(user is ManagerUser)
            {
				login.FeedBack = "שחר המנהל";
				login.Color = "blue";
				return View(login);
			}
			if (user is Barber)
			{
				login.FeedBack = "בר הספר";
				login.Color = "purple";
				return View(login);
			}
			return View(user);
        }
		public IActionResult Index()
        {
            DataLayer dataLayer= DataLayer.Instance;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}