using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class EmployeesViewModel : DataTableViewModel<Employee, DataHandlers.EmployeesDataHandler, Dialogs.EmployeeDialog>
    {
        public override string AddButtonIcon { get; set; } = "UserAdd";
        public override string RemoveButtonIcon { get; set; } = "UserRemove";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "First Name", PropertyBinding = new Binding("FirstName"), AllowSearch = true },
                new Misc.Column { Name = "Last Name", PropertyBinding = new Binding("LastName"), AllowSearch = true },
                new Misc.Column { Name = "Birth Date", PropertyBinding = new Binding("BirthDate") { StringFormat = "d" }, AllowSearch = true },
                new Misc.Column { Name = "Gender", PropertyBinding = new Binding("Gender") },
                new Misc.Column { Name = "Phone Number", PropertyBinding = new Binding("Phone"), AllowSearch = true },
                new Misc.Column { Name = "Postal Code", PropertyBinding = new Binding("Address.PostalCode"), AllowSearch = true },
                new Misc.Column { Name = "City", PropertyBinding = new Binding("Address.City"), AllowSearch = true },
                new Misc.Column { Name = "Street", PropertyBinding = new Binding("Address.Street"), AllowSearch = true },
                new Misc.Column { Name = "Employee Type", PropertyBinding = new Binding("EmployeeType.Name"), AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref Employee dataItem)
        {
            dataItem = new Employee
            {
                EmployeeTypeId = -1,
                BirthDate = DateTime.Today,
                Address = new Address()
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.EmployeeDialog dialog, ref Employee dataItem)
        {
            dialog = new Dialogs.EmployeeDialog
            {
                DataContext = new Dialogs.EmployeeDialogContext
                {
                    Employee = dataItem,
                    EmployeeTypes = dataHandler.GetEntities().EmployeeTypes.ToList()
                }
            };
        }
    }
}
