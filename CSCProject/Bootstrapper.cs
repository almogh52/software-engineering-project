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
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.LotsViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.PartsViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.PurchaseOrdersViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.PurchaseOrderPartsViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.SaleOrdersViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.SaleOrderPartsViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.InventoryViewModel", "CSCProject.Views.DataTableView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.ExpensesDistributionViewModel", "CSCProject.Views.ReportModelView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.SoldGoodsViewModel", "CSCProject.Views.ReportModelView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.MonthlySaleOrdersViewModel", "CSCProject.Views.ReportModelView");
            ViewLocator.NameTransformer.AddRule("CSCProject.ViewModels.MonthlyPurchaseOrdersViewModel", "CSCProject.Views.ReportModelView");
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