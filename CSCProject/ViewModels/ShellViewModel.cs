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

namespace CSCProject.ViewModels
{
    class ShellViewModel : Conductor<object>, INotifyPropertyChanged
    {
        public bool ViewsDrawerOpen { get; set; } = false;

        public Dictionary<string, Screen> ViewsList { get; set; } = new Dictionary<string, Screen>
        {
            { "Employees", new EmployeesViewModel() },
            { "Employee Types", new EmployeeTypesViewModel() }
        };

        public ShellViewModel()
        {
            // Transition to the initial view (Employees view)
            TransitionToView("Employees", ViewsList["Employees"]);
        }

        public void TransitionToView(string ViewName, Screen ViewScreen)
        {
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
