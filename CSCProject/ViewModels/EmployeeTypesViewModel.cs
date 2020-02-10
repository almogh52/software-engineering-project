using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.ViewModels
{
    class EmployeeTypesViewModel : Screen
    {
        private readonly dbEntities db = new dbEntities();

        public List<EmployeeType> EmployeeTypes { get => db.EmployeeTypes.ToList(); }

        public async void ShowNewEmployeeTypeDialog()
        {
            EmployeeType employeeType = new EmployeeType();

            Dialogs.NewEmployeeTypeDialog newEmployeeTypeDialog = new Dialogs.NewEmployeeTypeDialog
            {
                DataContext = employeeType
            };

            // Show the new employee type dialog
            await DialogHost.Show(newEmployeeTypeDialog, (object _, DialogClosingEventArgs args) =>
            {
                // Check if the dialog closing is a message dialog
                if (args.Session.Content.GetType() == typeof(Dialogs.MessageDialog))
                {
                    // Cancel the close of the dialog
                    args.Cancel();

                    // Show the new employee dialog type again
                    args.Session.UpdateContent(newEmployeeTypeDialog);
                }
                else
                {
                    // If not cancelled
                    if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                    {
                        try
                        {
                            // Add the employee type
                            AddEmployeeType(employeeType);
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

        private void AddEmployeeType(EmployeeType employeeType)
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

        private void UpdateTable()
        {
            // Notify table update
            NotifyOfPropertyChange("EmployeeTypes");
        }
    }
}
