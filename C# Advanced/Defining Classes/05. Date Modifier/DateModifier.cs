using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClasses
{
    public class DateModifier
    {
        private DateTime date1;
        private DateTime date2;

        public DateModifier(string date1, string date2)
        {
            this.date1 = DateTime.Parse(date1);
            this.date2 = DateTime.Parse(date2);
        }

        public int GetTimeSpanDifference()
        {
            int result = (int)(date1 - date2).TotalDays;
            return Math.Abs(result);
        }
    }
}
