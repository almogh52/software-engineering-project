using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class ExpensesViewModel : DataTableViewModel<Expense, DataHandlers.ExpensesDataHandler, Dialogs.ExpenseDialog>
    {
        public override string AddButtonIcon { get; set; } = "Add";
        public override string RemoveButtonIcon { get; set; } = "Remove";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description"), AllowSearch = true },
                new Misc.Column { Name = "Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true },
                new Misc.Column { Name = "Date", PropertyBinding = new Binding("Date") { StringFormat = "d" }, AllowSearch = true },
                new Misc.Column { Name = "Employee Id", PropertyBinding = new Binding("Employee.Id") },
                new Misc.Column { Name = "Employee First Name", PropertyBinding = new Binding("Employee.FirstName") },
                new Misc.Column { Name = "Employee Last Name", PropertyBinding = new Binding("Employee.LastName") },
            };
        }

        protected override void InitDataItem(ref Expense dataItem)
        {
            dataItem = new Expense
            {
                EmployeeId = -1,
                Date = DateTime.Now
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.ExpenseDialog dialog, ref Expense dataItem)
        {
            dialog = new Dialogs.ExpenseDialog
            {
                DataContext = new Dialogs.ExpenseDialogContext
                {
                    Expense = dataItem,
                    Employees = dataHandler.GetEntities().Employees.ToList().FindAll(employee => !employee.Deleted)
                }
            };
        }
    }
}
