using CSCProject.DataHandlers;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class PurchaseOrdersViewModel : DataTableViewModel<PurchaseOrder, DataHandlers.PurchaseOrdersDataHandler, Dialogs.PurchaseOrderDialog>
    {
        public override bool ShowDataItemId { get; set; } = true;

        public override bool HasDataContextMenuExtraButton { get; } = true;
        public override string DataContextMenuExtraButtonText { get; } = "Order Received";
        public override string DataContextMenuExtraButtonIcon { get; } = "TruckCheck";
        public override bool DataContextMenuExtraButtonEnabled { get => (GetView() as Views.DataTableView).Data.SelectedItem != null && !((PurchaseOrder)(GetView() as Views.DataTableView).Data.SelectedItem).Received; }

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Vendor Id", PropertyBinding = new Binding("VendorId"), AllowSearch = true },
                new Misc.Column { Name = "Vendor Name", PropertyBinding = new Binding("Vendor.Name"), AllowSearch = true },
                new Misc.Column { Name = "Order Date", PropertyBinding = new Binding("Date"), AllowSearch = true },
                new Misc.Column { Name = "Received", PropertyBinding = new Binding("Received"), AllowSearch = true },
                new Misc.Column { Name = "Total Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref PurchaseOrder dataItem)
        {
            dataItem = new PurchaseOrder
            {
                VendorId = -1,
                Date = DateTime.Now,
                Received = false
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.PurchaseOrderDialog dialog, ref PurchaseOrder dataItem)
        {
            dialog = new Dialogs.PurchaseOrderDialog
            {
                DataContext = new Dialogs.PurchaseOrderDialogContext
                {
                    PurchaseOrder = dataItem,
                    Vendors = dataHandler.GetEntities().Vendors.ToList().FindAll(type => !type.Deleted)
                }
            };
        }

        public override async void DataExtraButtonClicked(PurchaseOrder dataItem)
        {
            // If the order is empty
            if (dataItem.Parts.Count == 0)
            {
                await DialogHost.Show(new Dialogs.MessageDialog { Message = "Order is empty" });
                return;
            }

            Dialogs.SelectWarehouseDialog selectWarehouseDialog = new Dialogs.SelectWarehouseDialog 
            {
                DataContext = new Dialogs.SelectWarehouseDialogContext
                {
                    WarehouseId = -1,
                    Warehouses = dataHandler.GetEntities().Warehouses.ToList().FindAll(type => !type.Deleted)
                }
            };

            // Show the data item dialog
            await DialogHost.Show(selectWarehouseDialog, (object _, DialogClosingEventArgs args) =>
            {
                // Check if the dialog closing is a message dialog
                if (args.Session.Content.GetType() != typeof(Dialogs.MessageDialog))
                {
                    // If not cancelled
                    if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                    {
                        try
                        {
                            (dataHandler as PurchaseOrdersDataHandler).RecieveOrder(dataItem, (selectWarehouseDialog.DataContext as Dialogs.SelectWarehouseDialogContext).WarehouseId);
                            UpdateTable();
                            NotifyOfPropertyChange("DataContextMenuExtraButtonEnabled");
                        }
                        catch (Exception ex)
                        {
                            // Cancel the close of the dialog
                            args.Cancel();

                            // Show the error
                            args.Session.UpdateContent(new Dialogs.MessageDialog { Message = ex.Message });
                        }
                    }
                }
            });
        }
    }
}
