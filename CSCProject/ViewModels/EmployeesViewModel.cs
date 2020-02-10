using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CSCProject.ViewModels
{
    class EmployeesViewModel : Screen, INotifyPropertyChanged
    {
        private readonly dbEntities db = new dbEntities();

        public List<Employee> Employees { get => db.Employees.Where(e => !e.Deleted).ToList(); }
        
        public void RemoveEmployee(Employee employee)
        {
            // Set the employee as deleted
            employee.Deleted = true;

            // Set the entity as changed
            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

            // Save the datavase
            db.SaveChanges();

            // Update the table
            UpdateTable();
        }

        public async void ShowNewEmployeeDialog()
        {
            Employee employee = new Employee
            {
                EmployeeTypeId = -1,
                BirthDate = DateTime.Today,
                Address = new Address()
            };

            Dialogs.NewEmployeeDialog newEmployeeDialog = new Dialogs.NewEmployeeDialog
            {
                DataContext = new Dialogs.NewEmployeeDialogContext
                {
                    Employee = employee,
                    EmployeeTypes = db.EmployeeTypes.ToList()
                }
            };

            // Show the new employee dialog
            await DialogHost.Show(newEmployeeDialog, (object _, DialogClosingEventArgs args) =>
            {
                // Check if the dialog closing is a message dialog
                if (args.Session.Content.GetType() == typeof(Dialogs.MessageDialog))
                {
                    // Cancel the close of the dialog
                    args.Cancel();

                    // Show the new employee dialog again
                    args.Session.UpdateContent(newEmployeeDialog);
                }
                else
                {
                    // If not cancelled
                    if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                    {
                        try
                        {
                            // Add the employee
                            AddEmployee(employee);
                        }
                        catch (Exception ex)
                        {
                            // Cancel the close of the dialog
                            args.Cancel();

                            // Show the error
                            args.Session.UpdateContent(new Dialogs.MessageDialog { Message = ex.Message });
                        }

                        // Update the employees data
                        UpdateTable();
                    }
                }
            });
        }

        private void AddEmployee(Employee employee)
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

        private void UpdateTable()
        {
            // Notify table update
            NotifyOfPropertyChange("Employees");
        }

        private bool CheckIfAddressExists(int postalCode)
        {
            return db.Addresses.Count(a => a.PostalCode == postalCode) != 0;
        }
    }
}
