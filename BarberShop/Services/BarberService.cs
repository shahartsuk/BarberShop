﻿using BarberShop.Models;
using BarberShop.ViewModelBarber;

namespace BarberShop.Services
{
    public class BarberService
    {
        public VMSchedualProgram VM { get; set; }
        public List<MyDay> Days { get; set; } = new List<MyDay>();
        public void PlanProgram()
        {

            if (VM.workOnSunday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startSunday, end = VM.EndSunday });
            }
            if (VM.workOnMonday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startMonday, end = VM.EndMonday });
            }
            if (VM.workOnTuesday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startTuesday, end = VM.EndTuesday });
            }
            if (VM.workOnWednesday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startWednesday, end = VM.EndWednesday });
            }
            if (VM.workOnThursday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startThursday, end = VM.EndThursday });
            }
            if (VM.workOnFriday)
            {
                Days.Add(new MyDay { Day = 1, start = VM.startFriday, end = VM.EndFriday });
            }

            Barber barber = DataLayer.Instance.getBarbersAllIncludes.Find(b => b.IDRandom == VM.BarberID);
            if (barber == null) return;
        }
    }
    public class MyDay
    {
        public int Day { get; set; }

        public TimeOnly start { get; set; }
        public TimeOnly end { get; set; }
    }
}

