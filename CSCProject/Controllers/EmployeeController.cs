using CSCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.Controllers
{
    class EmployeeController
    {
        public List<Employee> GetEmployees()
        {
            return EmployeeModel.GetEmployees();
        }
        
        public List<EmployeeType> GetEmployeeTypes()
        {
            return EmployeeModel.GetEmployeeTypes();
        }

        public void AddEmployee(Employee employee)
        {
            EmployeeModel.AddEmployee(employee);
        }

        public void RemoveEmployee(int employeeId)
        {
            EmployeeModel.RemoveEmployee(employeeId);
        }

        public void AddEmployeeType(EmployeeType employeeType)
        {
            EmployeeModel.AddEmployeeType(employeeType);
        }
    }
}
