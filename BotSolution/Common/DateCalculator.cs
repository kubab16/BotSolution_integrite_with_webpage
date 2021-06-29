using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotSolution.Common
{
    public static class DateCalculator
    {      
        public static async Task<DateTime?> AddTime(string time)
        {
            if (!DateFormat(time)) return null;
            var EndDate = DateTime.Now;
            var times = Int32.Parse(time.Remove(time.Length - 1));
            switch (time[time.Length - 1])
            {
                case 'm':
                    
                    EndDate = EndDate.AddMinutes(times);
                    break;
                case 'h':
                    EndDate = EndDate.AddHours(times);
                    break;
                case 'd':
                    EndDate = EndDate.AddDays(times);
                    break;
                case 'M':
                    EndDate = EndDate.AddMonths(times);
                    break;
                case 'y':
                    EndDate = EndDate.AddYears(times);
                    break;
                default:
                    EndDate = EndDate.AddMilliseconds(times);
                    break;
            }
            return await Task.FromResult(EndDate);
        }
        public static async Task<DateTime?> AddTime(string time, DateTime Start)
        {           
            var times = Int32.Parse(time.Remove(time.Length - 1));
            var EndDate = Start;
            if (!DateFormat(time)) return null;
            switch (time[time.Length - 1])
            {
                case 'm':

                    EndDate = EndDate.AddMinutes(times);
                    break;
                case 'h':
                    EndDate = EndDate.AddHours(times);
                    break;
                case 'd':
                    EndDate = EndDate.AddDays(times);
                    break;
                case 'M':
                    EndDate = EndDate.AddMonths(times);
                    break;
                case 'y':
                    EndDate = EndDate.AddYears(times);
                    break;
                default:
                    EndDate = EndDate.AddMilliseconds(times);
                    break;  
            }
            return await Task.FromResult(EndDate);
        }
        public static bool DateFormat(string date)
        {
            if (date == null) return false;
            string dates = date.Remove(date.Length - 1);
            foreach (char DS in dates)
            {
                if(!char.IsDigit(DS) )
                {
                    return false;
                }
            }
            return char.IsLetter(date[date.Length - 1]);
        }
    }
}
