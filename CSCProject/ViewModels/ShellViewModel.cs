using Caliburn.Micro;
using CSCProject.Views;
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

        private bool _darkMode = false;
        public bool DarkMode { get { return _darkMode; }  set { _darkMode = value; ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light)); } }

        public string CurrentViewName { get; set; } = "";

        public List<TreeViewItem> TreeViewItems { get; set; } = new List<TreeViewItem>
        {
            new TreeViewItem { Header = "Home", Tag = new HomeViewModel() },
            new TreeViewItem { Header = "Employee Management", ItemsSource = new List<TreeViewItem> { 
                    new TreeViewItem { Header = "Employees", Tag = new EmployeesViewModel() },
                    new TreeViewItem { Header = "Employee Types", Tag = new EmployeeTypesViewModel() },
                    new TreeViewItem { Header = "Employee Expenses", Tag = new ExpensesViewModel() }
                } 
            },
            new TreeViewItem { Header = "Customer Management", ItemsSource = new List<TreeViewItem> {
                    new TreeViewItem { Header = "Customers", Tag = new CustomersViewModel() },
                    new TreeViewItem { Header = "Sale Orders", Tag = new SaleOrdersCombinedViewModel() }
                }
            },
            new TreeViewItem { Header = "Vendor Management", ItemsSource = new List<TreeViewItem> {
                    new TreeViewItem { Header = "Vendors", Tag = new VendorsViewModel() },
                    new TreeViewItem { Header = "Purchase Orders", Tag = new PurchaseOrdersCombinedViewModel() }
                }
            },
            new TreeViewItem { Header = "Factory Management", ItemsSource = new List<TreeViewItem> {
                    new TreeViewItem { Header = "Warehouses", Tag = new WarehousesViewModel() },
                    new TreeViewItem { Header = "Lots", Tag = new LotsViewModel() },
                    new TreeViewItem { Header = "Parts", Tag = new PartsViewModel() },
                    new TreeViewItem { Header = "Inventory", Tag = new InventoryViewModel() }
                }
            },
            new TreeViewItem { Header = "Reports", ItemsSource = new List<TreeViewItem> {
                    new TreeViewItem { Header = "Employee Expenses Distribution", Tag = new ExtendedExpensesDistributionViewModel() },
                    new TreeViewItem { Header = "Sold Goods", Tag = new ExtendedSoldGoodsViewModel() },
                    new TreeViewItem { Header = "Monthly Sale Orders", Tag = new ExtendedMonthlySaleOrdersViewModel() },
                    new TreeViewItem { Header = "Monthly Purchase Orders", Tag = new ExtendedMonthlyPurchaseOrdersViewModel() },
                    new TreeViewItem { Header = "Monthly Balance", Tag = new ExtendedMonthlyBalanceViewModel() }
                }
            },
        };

        public ShellViewModel()
        {
            // Transition to the initial view (Home view)
            TransitionToView("Home", (Screen)TreeViewItems[0].Tag);
        }

        public void TransitionToView(string ViewName, Screen ViewScreen)
        {
            if (ViewScreen == null)
            {
                return;
            }

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

        private void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
