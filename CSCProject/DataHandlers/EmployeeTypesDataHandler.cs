using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class EmployeeTypesDataHandler : Interfaces.IDataHandler<EmployeeType>
    {
        protected override void VerifyDataItem(EmployeeType dataItem)
        {
            Regex nameRegex = new Regex(@"^([a-zA-Z]+?)([-\s'][a-zA-Z]+)*?$");

            // Check if the name of the employee type is valid
            if (!nameRegex.IsMatch(dataItem.Name))
            {
                throw new ArgumentException("Invalid employee type name");
            }

            // Check if the salary is a valid integer
            if (dataItem.Salary <= 0)
            {
                throw new ArgumentException("Invalid salary");
            }
        }
    }
}
