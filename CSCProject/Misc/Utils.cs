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
                }
                else if (pi.PropertyType == typeof(DateTime))
                {
                    if (((DateTime)value).Date == DateTime.Today.Date)
                    {
                        return true;
                    }
                }
                else
                {
                    if (IsAnyNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Type FollowPropertyPath(Type type, string path)
        {
            Type currentType = type;

            foreach (string propertyName in path.Split('.'))
            {
                PropertyInfo property = currentType.GetProperty(propertyName);
                currentType = property.PropertyType;
            }

            return currentType;
        }
    }
}
