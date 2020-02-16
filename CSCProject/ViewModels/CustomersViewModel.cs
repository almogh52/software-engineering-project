using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class CustomersViewModel : DataTableViewModel<Customer, DataHandlers.CustomersDataHandler, Dialogs.CustomerDialog>
    {
        public override string AddButtonIcon { get; set; } = "UserAdd";
        public override string RemoveButtonIcon { get; set; } = "UserRemove";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Name", PropertyBinding = new Binding("Name"), AllowSearch = true },
                new Misc.Column { Name = "Phone Number", PropertyBinding = new Binding("Phone"), AllowSearch = true },
                new Misc.Column { Name = "Postal Code", PropertyBinding = new Binding("Address.PostalCode"), AllowSearch = true },
                new Misc.Column { Name = "City", PropertyBinding = new Binding("Address.City"), AllowSearch = true },
                new Misc.Column { Name = "Street", PropertyBinding = new Binding("Address.Street"), AllowSearch = true }
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.CustomerDialog dialog, ref Customer dataItem)
        {
            dialog = new Dialogs.CustomerDialog
            {
                DataContext = dataItem
            };
        }
    }
}
