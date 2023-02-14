using BarberShop.Models;
using BarberShop.Services;
using BarberShop.ViewModelBarber;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
	public class BarberController : Controller
	{
		public IActionResult Index(int? id)
		{
           Barber barber = DataLayer.Instance.Users.OfType<Barber>().ToList().Find(u=>u.IDRandom == id);

            return View(barber);
		}
        public IActionResult SelectAction()
        {
            List<HairCutActions> actions = DataLayer.Instance.HairCutActions.ToList();
            return View(new VMSelectAction { HaircutOptionalActions = actions, Barber = (Barber)DataLayer.Instance.CurrUser, BarberID = DataLayer.Instance.CurrUser.ID });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SelectAction(VMSelectAction VM)
        {
            ActionPerBarber haircutActionsPerBarber = new ActionPerBarber();
            haircutActionsPerBarber.Action = DataLayer.Instance.HairCutActions.FirstOrDefault(g => g.ID == VM.HaircutActionID);

            if (haircutActionsPerBarber != null && VM != null)
            {
                haircutActionsPerBarber.Barber = DataLayer.Instance.Users.OfType<Barber>().ToList().FirstOrDefault(g => g.ID == VM.BarberID);
                haircutActionsPerBarber.ActionDuration = VM.ActionDuration;
                haircutActionsPerBarber.Price = VM.Price;
                haircutActionsPerBarber.PercentPerWork = VM.PercentFromTotalActions;
                haircutActionsPerBarber.Barber.Actions.Add(haircutActionsPerBarber);
                DataLayer.Instance.ActionPerBarber.Add(haircutActionsPerBarber);
                DataLayer.Instance.SaveChanges();

            }

            return RedirectToAction("Index", "home");

        }

        public IActionResult MyActions(int? id)
        {


            Barber barber = DataLayer.Instance.getBarbersAllIncludes.Find(b => b.IDRandom == id);

            List<ActionPerBarber> myActions = barber.Actions;


            return View(myActions);
        }

        //פונקצית הוספת תוכנית תאריכית
        public IActionResult addProgram(int? id)
        {
            if (id == null) return RedirectToAction("index", "home");
            User user = DataLayer.Instance.Users.ToList().Find(u => u.IDRandom == id);
            if (user == null) return RedirectToAction("index", "home");
            user.RND = 0;
            DataLayer.Instance.SaveChanges();
            DataLayer.Instance.CurrUser = user;
            VMSchedualProgram vMSchedual = new VMSchedualProgram
            {
                Barber = (Barber)user,
                BarberID = user.RND
            };
            return View(vMSchedual);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult addProgram(VMSchedualProgram VM)
        {
            new BarberService { VM = VM }.PlanProgram();
            DataLayer.Instance.SaveChanges();
            return RedirectToAction("Calender", new {id = VM.BarberID});

        }
		public IActionResult Calender(int? id)
        {
            Barber barber = DataLayer.Instance.getBarbersAllIncludes.Find(u=>u.IDRandom == id);
            return View(new VMCalander { Appointments = barber.Appointments});
        }
        
    }
}
