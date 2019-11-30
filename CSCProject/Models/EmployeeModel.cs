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

        public static void AddEmployee(Employee employee)
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
            
            // Set the address of the employee
            employee.PostalCode = employee.Address.PostalCode;

            // Check if the address already exists in the db
            if (CheckIfAddressExists(employee.PostalCode))
            {
                // Remove address to prevent creation of new address
                employee.Address = null;
            }

            // Add the employee
            db.Employees.Add(employee);

            // Save changes to the database
            db.SaveChanges();
        }

        public static void RemoveEmployee(int employeeId)
        {
            Employee employee = db.Employees.SingleOrDefault(e => e.Id == employeeId);

            // Check if the employee is found
            if (employee.Id != employeeId)
            {
                throw new ArgumentException("Employee not found");
            }

            // Set the employee as deleted
            employee.Deleted = true;

            // Set the entity as changed
            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

            // Save the datavase
            db.SaveChanges();
        }

        public static void AddEmployeeType(EmployeeType employeeType)
        {
            Regex nameRegex = new Regex(@"^([a-zA-Z]+?)([-\s'][a-zA-Z]+)*?$");

            // Check if the name of the employee type is valid
            if (!nameRegex.IsMatch(employeeType.Name))
            {
                throw new ArgumentException("Invalid employee type name");
            }

            // Add the employee type
            db.EmployeeTypes.Add(employeeType);

            // Save changes to the database
            db.SaveChanges();
        }

        private static bool CheckIfAddressExists(int postalCode)
        {
            return db.Addresses.Count(a => a.PostalCode == postalCode) != 0;
        }
    }
}
