namespace CSCProject {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using Caliburn.Micro;

    public class Bootstrapper : BootstrapperBase {
        public Bootstrapper() {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                Misc.PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");

            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.EmployeesViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.EmployeeTypesViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.VendorsViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.CustomersViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.WarehousesViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.ExpensesViewModel", "CSCProject.Views.DataTableView");
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
#if DEBUG
            DisplayRootViewFor<ViewModels.ShellViewModel>();
#else
            DisplayRootViewFor<ViewModels.ConnectionViewModel>();
#endif
        }
    }
}