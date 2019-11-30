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

namespace CSCProject.Dialogs
{
    public class NewEmployeeDialogContext
    {
        public Employee Employee { get; set; }
        public List<EmployeeType> EmployeeTypes { get; set; }
    }

    /// <summary>
    /// Interaction logic for NewEmployeeDialog.xaml
    /// </summary>
    public partial class NewEmployeeDialog : UserControl
    {
        public ICommand CloseCommand { get; } = new Misc.RelayCommand(o => DialogHost.CloseDialogCommand.Execute(true, null), o => !Misc.Utils.IsAnyNullOrEmpty((Employee)o));

        public NewEmployeeDialog()
        {
            InitializeComponent();
        }
    }
}
