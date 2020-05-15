using CSCProject.DataHandlers;
using MaterialDesignThemes.Wpf;
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

        public override bool HasDataContextMenuExtraButton { get; } = true;
        public override string DataContextMenuExtraButtonText { get; } = "Order Shipped";
        public override string DataContextMenuExtraButtonIcon { get; } = "TruckCheck";
        public override bool DataContextMenuExtraButtonEnabled { get => (GetView() as Views.DataTableView).Data.SelectedItem != null && !((SaleOrder)(GetView() as Views.DataTableView).Data.SelectedItem).Shipped; }

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Customer Id", PropertyBinding = new Binding("CustomerId"), AllowSearch = true },
                new Misc.Column { Name = "Customer Name", PropertyBinding = new Binding("Customer.Name"), AllowSearch = true },
                new Misc.Column { Name = "Order Date", PropertyBinding = new Binding("Date"), AllowSearch = true },
                new Misc.Column { Name = "Shipped", PropertyBinding = new Binding("Shipped"), AllowSearch = true },
                new Misc.Column { Name = "Total Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref SaleOrder dataItem)
        {
            dataItem = new SaleOrder
            {
                CustomerId = -1,
                Date = DateTime.Now,
                Shipped = false
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

        public override async void DataExtraButtonClicked(SaleOrder dataItem)
        {
            // If the order is empty
            if (dataItem.Parts.Count == 0)
            {
                await DialogHost.Show(new Dialogs.MessageDialog { Message = "Order is empty" });
                return;
            }

            // Try to ship the order
            try
            {
                (dataHandler as SaleOrdersDataHandler).ShipOrder(dataItem);
                UpdateTable();
                NotifyOfPropertyChange("DataContextMenuExtraButtonEnabled");
            }
            catch (Exception ex)
            {
                // Show the error
                await DialogHost.Show(new Dialogs.MessageDialog { Message = ex.Message });
            }
        }
    }
}
