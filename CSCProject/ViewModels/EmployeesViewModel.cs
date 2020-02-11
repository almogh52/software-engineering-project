using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class EmployeesViewModel : DataTableViewModel<Employee, DataHandlers.EmployeesDataHandler, Dialogs.NewEmployeeDialog>
    {
        public override string AddButtonIcon { get; set; } = "UserAdd";
        public override string RemoveButtonIcon { get; set; } = "UserRemove";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "First Name", PropertyBinding = new Binding("FirstName") },
                new Misc.Column { Name = "Last Name", PropertyBinding = new Binding("LastName") },
                new Misc.Column { Name = "Birth Date", PropertyBinding = new Binding("BirthDate") { StringFormat = "d" } },
                new Misc.Column { Name = "Gender", PropertyBinding = new Binding("Gender") },
                new Misc.Column { Name = "Phone Number", PropertyBinding = new Binding("Phone") },
                new Misc.Column { Name = "Postal Code", PropertyBinding = new Binding("Address.PostalCode") },
                new Misc.Column { Name = "City", PropertyBinding = new Binding("Address.City") },
                new Misc.Column { Name = "Street", PropertyBinding = new Binding("Address.Street") },
                new Misc.Column { Name = "Employee Type", PropertyBinding = new Binding("EmployeeType.Name") },
                new Misc.Column { Name = "Deleted", PropertyBinding = new Binding("Deleted") }
            };
        }

        protected override void InitNewDataItemDialog(ref Dialogs.NewEmployeeDialog dialog, ref Employee dataItem)
        {
            dataItem = new Employee
            {
                EmployeeTypeId = -1,
                BirthDate = DateTime.Today,
                Address = new Address()
            };

            dialog = new Dialogs.NewEmployeeDialog
            {
                DataContext = new Dialogs.NewEmployeeDialogContext
                {
                    Employee = dataItem,
                    EmployeeTypes = dataHandler.GetEntities().EmployeeTypes.ToList()
                }
            };
        }
    }
}
