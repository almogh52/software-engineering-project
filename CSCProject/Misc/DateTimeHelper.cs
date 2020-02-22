using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.Misc
{
    public static class DateTimeHelper
    {
        public static DateTime Yesterday
        {
            get { return DateTime.Today.AddDays(-1); }
        }

        public static DateTime Tomorrow
        {
            get { return DateTime.Today.AddDays(1); }
        }
    }
}
