using BarberShop.Models;

namespace BarberShop.ViewModelBarber
{
    public class VMCalander
    {
        public VMCalander() { }

        private List<Appointment> _Appointments { get; set; }
        public List<Appointment> Appointments { get{ return _Appointments.ToList();}
            set
            {
                _Appointments = value.FindAll(a => a.DateTime >= DateTime.Now && a.DateTime.Date <= DateTime.Now.AddDays(10).Date);
            }
        }
        //רשימה של כל התורים מחולקים למערכים של כל יום
        public List<Appointment[]> DailyAppointments { get
            {
                //בניית רשימה של מערכים לשליחה בפונקציה
                List<Appointment[]> MyDaily = new List<Appointment[]>();
                
                //ריצה על כל הימים שבטווח של הפגישות
                for (DateTime i = Appointments.First().DateTime.Date;i  < Appointments.Last().DateTime.Date; i=i.AddDays(1))
                {
                    //יצירת רשימה יומית
                    Appointment[] temp =Appointments.FindAll(a=>a.DateTime.Date == i.Date).ToArray();
                    //בדיקה האם יש פגישות באותו יום
                    if(temp.Length > 0)
                    {
                        MyDaily.Add(temp);
                    }
                    
                }
                return MyDaily;
            } }
    }
}
