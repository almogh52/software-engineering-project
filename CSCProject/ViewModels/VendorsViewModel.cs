using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class VendorsViewModel : DataTableViewModel<Vendor, DataHandlers.VendorsDataHandler, Dialogs.NewVendorDialog>
    {
        public override string AddButtonIcon { get; set; } = "BankAdd";
        public override string RemoveButtonIcon { get; set; } = "BankRemove";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Name", PropertyBinding = new Binding("Name") },
                new Misc.Column { Name = "Phone Number", PropertyBinding = new Binding("Phone") },
                new Misc.Column { Name = "Postal Code", PropertyBinding = new Binding("Address.PostalCode") },
                new Misc.Column { Name = "City", PropertyBinding = new Binding("Address.City") },
                new Misc.Column { Name = "Street", PropertyBinding = new Binding("Address.Street") },
                new Misc.Column { Name = "Deleted", PropertyBinding = new Binding("Deleted") }
            };
        }

        protected override void InitNewDataItemDialog(ref Dialogs.NewVendorDialog dialog, ref Vendor dataItem)
        {
            dataItem = new Vendor
            {
                Address = new Address()
            };

            dialog = new Dialogs.NewVendorDialog
            {
                DataContext = dataItem
            };
        }
    }
}
