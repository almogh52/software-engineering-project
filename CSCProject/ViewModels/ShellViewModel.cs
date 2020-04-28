using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class ShellViewModel : Conductor<object>, INotifyPropertyChanged
    {
        public bool ViewsDrawerOpen { get; set; } = false;

        public string CurrentViewName { get; set; } = "";

        public Dictionary<string, Screen> ViewsList { get; set; } = new Dictionary<string, Screen>
        {
            { "Employees", new EmployeesViewModel() },
            { "Employee Types", new EmployeeTypesViewModel() },
            { "Vendors", new VendorsViewModel() },
            { "Customers", new CustomersViewModel() },
            { "Warehouses", new WarehousesViewModel() },
            { "Expenses", new ExpensesViewModel() },
            { "Lots", new LotsViewModel() },
            { "Parts", new PartsViewModel() },
            { "Inventory", new InventoryViewModel() },
            { "Purchase Orders", new PurchaseOrdersViewModel() },
            { "Purchase Order Parts", new PurchaseOrderPartsViewModel() },
            { "Sale Orders", new SaleOrdersViewModel() },
            { "Sale Order Parts", new SaleOrderPartsViewModel() },
            { "Expenses Distribution", new ExpensesDistributionViewModel() }
        };

        public ShellViewModel()
        {
            // Transition to the initial view (Employees view)
            TransitionToView("Employees", ViewsList["Employees"]);
        }

        public void TransitionToView(string ViewName, Screen ViewScreen)
        {
            // Set the current view name
            CurrentViewName = ViewName;

            // Set the new display name of the app
            DisplayName = $"Core Scientific Creations - {ViewName}";

            // Activate the item
            ActivateItem(ViewScreen);

            // Close the views drawer if open
            ViewsDrawerOpen = false;
        }

        public async void ShowAboutDialog()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            // Show the about message dialog
            await DialogHost.Show(new Dialogs.MessageDialog { Message = $"CSC Database\nVersion {version}\n© 2020 Almog Hamdani.\nAll rights reserved." });
        }

        public void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
