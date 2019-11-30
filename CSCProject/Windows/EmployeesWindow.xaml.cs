﻿using MaterialDesignThemes.Wpf;
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
        }

        private async void AddEmployeeBtnClicked(object sender, RoutedEventArgs e)
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
    }
}
