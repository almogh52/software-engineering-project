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

        public void AddEmployee(Employee employee, Address address)
        {
            EmployeeModel.AddEmployee(employee, address);
        }

        public void RemoveEmployee(int employeeId)
        {
            EmployeeModel.RemoveEmployee(employeeId);
        }
    }
}
