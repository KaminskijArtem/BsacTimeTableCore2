using System;

namespace BsacTimeTableCore2.Services
{
    public class AcessoryService : IAcessoryService
    {
        public int GetCurrentWeek()
        {
            DateTime dtNow = new DateTime();
            dtNow = DateTime.Now;
            int year;


            if (dtNow.Month < 9)
                year = dtNow.Year - 1;
            else
                year = dtNow.Year;

            DateTime dtSept = new DateTime(year, 9, 1);

            TimeSpan diff = dtNow - dtSept;
            int differenceInDays = diff.Days;
            differenceInDays += (int)dtSept.DayOfWeek - 1;
            int currentWeek = 1 + (differenceInDays % 28) / 7;
            return currentWeek;
        }
    }
}