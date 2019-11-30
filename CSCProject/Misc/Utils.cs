using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.Misc
{
    public static class Utils
    {
        public static bool IsAnyNullOrEmpty(object myObject)
        {
            if (myObject == null)
            {
                return false;
            }

            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                object value = pi.GetValue(myObject);

                if (pi.PropertyType == typeof(string))
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        return true;
                    }
                } else
                {
                    if (IsAnyNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
