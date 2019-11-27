using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.Models
{
    class EmployeeModel
    {
        private static readonly dbEntities db = new dbEntities();

        public static List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public static List<EmployeeType> GetEmployeeTypes()
        {
            return db.EmployeeTypes.ToList();
        }

        public static void AddEmployee(Employee employee, Address address)
        {
            Regex nameRegex = new Regex(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$");

            // Check if the name of the employee is valid
            if (!nameRegex.IsMatch(employee.FirstName) || !nameRegex.IsMatch(employee.LastName))
            {
                throw new System.ArgumentException("Invalid employee name");
            }

            // Check for valid address
            if (!nameRegex.IsMatch(address.City))
            {
                throw new System.ArgumentException("Invalid city name");
            }
            else if (!nameRegex.IsMatch(address.Street))
            {
                throw new System.ArgumentException("Invalid street name");
            }

            try
            {
                // Add the address to the address list
                db.Addresses.Add(address);
            }
            catch
            {
                throw new System.ArgumentException("Address already taken");
            }
            
            // Set the address of the employee
            employee.PostalCode = address.PostalCode;

            // Add the employee
            db.Employees.Add(employee);

            // Save changes to the database
            db.SaveChanges();
        }

        public static void RemoveEmployee(int employeeId)
        {
            Employee employee = (from e in db.Employees where e.Id == employeeId select e).FirstOrDefault();

            // Check if the employee is found
            if (employee.Id != employeeId)
            {
                throw new System.ArgumentException("Employee not found");
            }

            // Remove the employee
            db.Employees.Remove(employee);

            // Save the datavase
            db.SaveChanges();
        }
    }
}
