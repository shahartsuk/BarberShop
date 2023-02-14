using BarberShop.Models;
using BarberShop.ViewModelManager;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace BarberShop.Controllers
{
    public class ManagerController : Controller
	{
		//פונקציה המוסיפה ספר
		public IActionResult AddBarber()
		{
            VMIsActiveBarbers VMUsers = new VMIsActiveBarbers(DataLayer.Instance.Users.ToList());
            return View(VMUsers);
		}
        public IActionResult CreateBarber(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            List<User> users = DataLayer.Instance.Users.ToList();

            ClientUser client = users.OfType<ClientUser>().ToList().Find(User => User.ID == id);

            DataLayer.Instance.Database.ExecuteSqlCommand("UPDATE BarberShop.dbo.Users SET Discriminator = 'Barber' WHERE ID = {0}", id);

            DataLayer.Instance.SaveChanges();

            //VMIsActiveBarber vm = new VMIsActiveBarber(DataLayer.Data.Users.ToList());


            return View("index");
        }
        public IActionResult Barbers()
        {
            VMIsActiveBarbers VMBarbers = new VMIsActiveBarbers(DataLayer.Instance.Users.ToList());
            return View(VMBarbers);
        }
        public IActionResult ChangeActive(int? id)
        {
            //להביא רשימה של המשתמשים ולא רק הספרים
            //בשביל לבנות את המודל ולהחזיר אותו בVIEW
            //ובכך לחסוך פניה למסד נתונים
            List<User> barbers = DataLayer.Instance.Users.ToList();

            if(id != null)
            {
                Barber barber = (Barber)barbers.Find(b => b.ID == id);

                if (barber != null && barber is Barber) barber.Active = !barber.Active;

                DataLayer.Instance.SaveChanges();
            }

            return View("Barbers", new VMIsActiveBarbers(barbers));
        }

        //פונקציה המוסיפה פעולה ומציגה את כל הפעולות
        public IActionResult Actions(int? id)
        {
            List<HairCutActions> actions = DataLayer.Instance.HairCutActions.ToList();
            return View(new VMCreateHairCut {ActionList = actions });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Actions(VMCreateHairCut VM)
        {
            DataLayer.Instance.HairCutActions.Add(VM.Action);
            VM.Action.SetImage = VM.File;
            DataLayer.Instance.SaveChanges();
            return RedirectToAction("Actions");
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                DataLayer.Instance.CurrUser = new ClientUser { FirstName = "התחבר" };
                return RedirectToAction("Index", "Home");
            }
            User user = DataLayer.Instance.Users.FirstOrDefault(u => u.IDRandom == id);
            if (user != null)
                if (user is ManagerUser)
                {
                    DataLayer.Instance.CurrUser = user;
                    user.RND = 0;
                    DataLayer.Instance.SaveChanges();
                    return View();
                }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowAllUsers()
        {
            List<User> users = DataLayer.Instance.Users.ToList();
            return View(users);

        }
    }
}
