using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class EmployeeTypesViewModel : DataTableViewModel<EmployeeType, DataHandlers.EmployeeTypesDataHandler, Dialogs.EmployeeTypeDialog>
    {
        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Name", PropertyBinding = new Binding("Name"), AllowSearch = true },
                new Misc.Column { Name = "Monthly Salary", PropertyBinding = new Binding("Salary"), AllowSearch = true },
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description"), AllowSearch = true }
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.EmployeeTypeDialog dialog, ref EmployeeType dataItem)
        {
            // Init new employee type dialog object
            dialog = new Dialogs.EmployeeTypeDialog
            {
                DataContext = dataItem
            };
        }
    }
}
