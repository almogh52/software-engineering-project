using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        public static bool CheckIfAddressExists(dbEntities db, int postalCode)
        {
            return db.Addresses.Count(a => a.PostalCode == postalCode) != 0;
        }

        public static bool VerifyName(string name)
        {
            Regex nameRegex = new Regex(@"^([a-zA-Z]+?)([-\s'][a-zA-Z]+)*?$");

            return nameRegex.IsMatch(name);
        }

        public static bool VerifyPhone(string phone)
        {
            Regex phoneRegex = new Regex(@"^\+?(972|0)(\-)?0?(([23489]{1}\d{7})|[5]{1}\d{8})$");

            return phoneRegex.IsMatch(phone);
        }
    }
}
