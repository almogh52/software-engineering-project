using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSCProject.Windows
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private readonly Controllers.EmployeeController controller = new Controllers.EmployeeController();

        public EmployeesWindow()
        {
            InitializeComponent();

            // Update the employees view
            Update();
        }

        private void Update()
        {
            // Get the employees
            employeesDataGrid.ItemsSource = controller.GetEmployees();

            // Get the employee types
            employeeTypesDataGrid.ItemsSource = controller.GetEmployeeTypes();
        }

        private async void NewEmployeeBtnClicked(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee
            {
                Address = new Address()
            };

            Dialogs.NewEmployeeDialog newEmployeeDialog = new Dialogs.NewEmployeeDialog
            {
                DataContext = new Dialogs.NewEmployeeDialogContext
                {
                    Employee = employee,
                    EmployeeTypes = controller.GetEmployeeTypes()
                }
            };

            // Show the new employee dialog
            await DialogHost.Show(newEmployeeDialog, (object _, DialogClosingEventArgs args) =>
            {
                // If not cancelled
                if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                {
                    // Add the employee
                    controller.AddEmployee(employee);

                    // Update the employees data
                    Update();
                }
            });
        }

        private void RemoveEmployeeBtnClicked(object sender, RoutedEventArgs e)
        {
            Employee employee = employeesDataGrid.SelectedItem as Employee;

            // If the employee isn't null
            if (employee != null)
            {
                // Remove the employee
                controller.RemoveEmployee(employee.Id);

                // Update the employees data
                Update();
            }
        }

        private async void NewEmployeeTypeBtnClicked(object sender, RoutedEventArgs e)
        {
            EmployeeType employeeType = new EmployeeType();

            Dialogs.NewEmployeeTypeDialog newEmployeeTypeDialog = new Dialogs.NewEmployeeTypeDialog
            {
                DataContext = employeeType
            };

            // Show the new employee type dialog
            await DialogHost.Show(newEmployeeTypeDialog, (object _, DialogClosingEventArgs args) =>
            {
                // If not cancelled
                if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                {
                    // Add the employee type
                    controller.AddEmployeeType(employeeType);

                    // Update the employees data
                    Update();
                }
            });
        }

        private void RemoveEmployeeTypeBtnClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
