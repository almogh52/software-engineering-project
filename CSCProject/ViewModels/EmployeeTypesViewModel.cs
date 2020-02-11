using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class EmployeeTypesViewModel : DataTableViewModel<EmployeeType, DataHandlers.EmployeeTypesDataHandler, Dialogs.NewEmployeeTypeDialog>
    {
        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Name", PropertyBinding = new Binding("Name") },
                new Misc.Column { Name = "Monthly Salary", PropertyBinding = new Binding("Salary") },
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description") },
                new Misc.Column { Name = "Deleted", PropertyBinding = new Binding("Deleted") }
            };
        }

        protected override void InitNewDataItemDialog(ref Dialogs.NewEmployeeTypeDialog dialog, ref EmployeeType dataItem)
        {
            // Init employee type object
            dataItem = new EmployeeType();

            // Init new employee type dialog object
            dialog = new Dialogs.NewEmployeeTypeDialog
            {
                DataContext = dataItem
            };
        }
    }
}
