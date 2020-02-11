namespace CSCProject {
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;

    public class Bootstrapper : BootstrapperBase {
        public Bootstrapper() {
            Initialize();

            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.EmployeesViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.EmployeeTypesViewModel", "CSCProject.Views.DataTableView");
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<ViewModels.ShellViewModel>();
        }
    }
}