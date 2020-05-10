using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class SaleOrdersViewModel : DataTableViewModel<SaleOrder, DataHandlers.SaleOrdersDataHandler, Dialogs.SaleOrderDialog>
    {
        public override bool ShowDataItemId { get; set; } = true;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Customer Id", PropertyBinding = new Binding("CustomerId"), AllowSearch = true },
                new Misc.Column { Name = "Customer Name", PropertyBinding = new Binding("Customer.Name"), AllowSearch = true },
                new Misc.Column { Name = "Order Date", PropertyBinding = new Binding("Date"), AllowSearch = true },
                new Misc.Column { Name = "Total Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref SaleOrder dataItem)
        {
            dataItem = new SaleOrder
            {
                CustomerId = -1,
                Date = DateTime.Now
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.SaleOrderDialog dialog, ref SaleOrder dataItem)
        {
            dialog = new Dialogs.SaleOrderDialog
            {
                DataContext = new Dialogs.SaleOrderDialogContext
                {
                    SaleOrder = dataItem,
                    Customers = dataHandler.GetEntities().Customers.ToList().FindAll(type => !type.Deleted)
                }
            };
        }
    }
}
