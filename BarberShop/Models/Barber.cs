using BarberShop.Services;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BarberShop.Models
{
    public class Barber : User
    {
        public Barber() {
            Actions = new List<ActionPerBarber>();
            Appointments = new List<Appointment>();
        }
        [Display(Name ="תיאור")]
        public string Description { get; set; }

        [Display(Name="תמונת הספר")]
        public byte[]? MyImage { get; set; }

        //תכונת הוספה של תמונה
        public IFormFile setImage{ set{ MyImage = new SetImg(value).MyImage;}}
        //רשימה של כל הפעולות שהספר מבצע

        public List<Appointment> Appointments { get; set; }
        public List<ActionPerBarber>? Actions { get; set; }
       //פונקציה המוסיפה פעולה
       public void AddHairCutAction(string HairCut,int price,int actionDuration,int perecent)
       {
            //מציאת הסוג תספורת הנכונה מהמסד נתונים
            HairCutActions hairCutAction = DataLayer.Instance.HairCutActions.ToList().Find(a => a.Name == HairCut);
            //בדיקה אם מה שקיבלנו הוא נכון
            if (hairCutAction == null) return;
			//להוסיף תספורת לספר
			ActionPerBarber actionPerBarber = new ActionPerBarber { Action = hairCutAction, ActionDuration = actionDuration, Price = price, PercentPerWork = perecent, Barber = this };
			Actions.Add(actionPerBarber);
            //הוספה לרשימה של הפעולות
            hairCutAction.PerBarbers.Add(actionPerBarber);
	   }

        //פונקציה המקבלת טווח חודשי כולל השעות לפי הימים בשבוע
        [NotMapped]
        //החזרת מחזוריות
        private int sumPlan
        {
            get
            {
                int sum=0;
                foreach (int i in plan)
                {
                    sum += i;
                }
                return sum;
            }
        }
        [NotMapped]
        //החזרת רשימה של כמות כל אחת מהפעולות
        private List<int> plan = new List<int>();
        //פונקציה המוסיפה פגישות לפי כמה ימים בשבוע
        public void AddWeeklyAppoitments(DateTime start,DateTime end,List<MyDay> days)
        {
            int counter = 0;
            for(DateTime i = start;i <= end; i = i.AddDays(1))
            {
                MyDay myDay = days.Find(d=>d.Day == (int)i.DayOfWeek);
                if(myDay != null)
                {
                    DateTime dayStart = new DateTime(i.Year, i.Month, i.Day, myDay.start.Hour, myDay.start.Minute, 0);
                    DateTime dayEnd = new DateTime(i.Day, i.Month, i.Day,myDay.end.Hour, myDay.end.Minute,0);
                    AddDailyAppointment(dayStart, dayEnd);
                }
            }
        }
         
        //פונקציה המקבלת טווח תאריכי עם שעות של יום בשבוע
        public void AddRangeDaily(DateTime start, DateTime end,int dayOfTheWeek,TimeOnly timeStart,TimeOnly timeEnd)
        {
            for (DateTime day = start; day <= end; day = day.AddDays(1))
            {
                if((int)day.DayOfWeek == dayOfTheWeek)
                {
                    AddDailyAppointment(new DateTime(day.Year, day.Month, day.Day, timeStart.Hour, timeStart.Minute,0), new DateTime(day.Year, day.Month, day.Day, timeEnd.Hour, timeEnd.Minute, 0));
                }
            }
        }
        public void AddDailyAppointment(DateTime start,DateTime end)
        {
            DateTime Time = start;
            //תכנון פגישות
            List<int> plan = new List<int>();

            //הזמן של היום עבודה שלו בדקות
            int day = (end.Hour - start.Hour) * 60;

            foreach (ActionPerBarber hairCut in Actions)
            {
                //חילוק היום עבודה לפי האחוזים של הפעולות של הספר
                int time = (day / 100) * hairCut.PercentPerWork;
                plan.Add(time);
            }
            foreach (ActionPerBarber hairCut in Actions)
            {
                int i = 0;
                //כמה פעמים נכנסת הפעולה בחלקי היום
                int partOfDay = plan[i] / hairCut.ActionDuration;
                //אם נשאר כמה דקות אחרי שעושה את הפעולות בחלק של היום להוסיף אותו לחלק הבא
                int rest = plan[i] % hairCut.ActionDuration;
                if (i < plan.Count -1)
                {
                    //להוסיף את שארית הזמן מהחלק הקודם
                    plan[i + 1] += rest;
                }
                if(i < plan.Count)
                {
                    i++;
                }
                for (int j = 0; j < partOfDay; j++)
                {
                    Time = Time.AddMinutes(hairCut.ActionDuration);
                    AddAppointment(hairCut, Time);
                }
            }
        }
        public Appointment AddAppointment(string hairCut,DateTime dateTime)
        {
            ActionPerBarber BarberHairCut = Actions.Find(h=>h.Action.Name == hairCut);
            return AddAppointment(hairCut, dateTime);
        }
        public Appointment AddAppointment(ActionPerBarber HairCut,DateTime dateTime)
        {
            Appointment appointment = new Appointment { DateTime = dateTime, HairCut = HairCut };
            Appointments.Add(appointment);
            return appointment;
        }

    }
}
//[Display(Name ="תמונות של תספורות")] 
//public List<Image> HairCutsImages { get; set; }
//    //פונקציה של הוספת תמונה
//public void AddImage(IFormFile file)
//{
//    if (file == null) return;
//    HairCutsImages.Add(new Image { barber = this, setImage = file });
//}
