using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class EmployeesDataHandler : Interfaces.IDataHandler<Employee>
    {
        public override void AddDataItem(Employee dataItem)
        {
            // Set the address of the employee
            dataItem.PostalCode = dataItem.Address.PostalCode;

            // Check if the address already exists in the db
            if (CheckIfAddressExists(dataItem.PostalCode))
            {
                // Remove address to prevent creation of new address
                dataItem.Address = null;
            }

            // Call base add data item
            base.AddDataItem(dataItem);
        }

        protected override void VerifyDataItem(Employee employee)
        {
            Regex nameRegex = new Regex(@"^([a-zA-Z]+?)([-\s'][a-zA-Z]+)*?$");
            Regex phoneRegex = new Regex(@"^\+?(972|0)(\-)?0?(([23489]{1}\d{7})|[5]{1}\d{8})$");

            // Check if the name of the employee is valid
            if (!nameRegex.IsMatch(employee.FirstName) || !nameRegex.IsMatch(employee.LastName))
            {
                throw new ArgumentException("Invalid employee name");
            }

            // Check for valid phone number
            if (!phoneRegex.IsMatch(employee.Phone))
            {
                throw new ArgumentException("Invalid phone number");
            }

            // Check for valid address
            if (!nameRegex.IsMatch(employee.Address.City))
            {
                throw new ArgumentException("Invalid city name");
            }
        }

        private bool CheckIfAddressExists(int postalCode)
        {
            return db.Addresses.Count(a => a.PostalCode == postalCode) != 0;
        }
    }
}
