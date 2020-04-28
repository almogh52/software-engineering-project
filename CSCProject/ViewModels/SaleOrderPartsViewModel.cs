using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class SaleOrderPartsViewModel : DataTableViewModel<SaleOrderPart, DataHandlers.SaleOrderPartsDataHandler, Dialogs.SaleOrderPartDialog>
    {
        public override bool HasId { get; set; } = false;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Order Id", PropertyBinding = new Binding("OrderId"), AllowSearch = true },
                new Misc.Column { Name = "Part Id", PropertyBinding = new Binding("PartId"), AllowSearch = true },
                new Misc.Column { Name = "Part Description", PropertyBinding = new Binding("Part.Description"), AllowSearch = true },
                new Misc.Column { Name = "Lot Id", PropertyBinding = new Binding("LotId"), AllowSearch = true },
                new Misc.Column { Name = "Quantity", PropertyBinding = new Binding("Quantity"), AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref SaleOrderPart dataItem)
        {
            dataItem = new SaleOrderPart
            {
                LotId = "",
                OrderId = -1,
                PartId = -1
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.SaleOrderPartDialog dialog, ref SaleOrderPart dataItem)
        {
            dialog = new Dialogs.SaleOrderPartDialog
            {
                DataContext = new Dialogs.SaleOrderPartDialogContext
                {
                    SaleOrderPart = dataItem,
                    Orders = dataHandler.GetEntities().SaleOrders.ToList().FindAll(type => !type.Deleted),
                    Parts = dataHandler.GetEntities().Parts.ToList().FindAll(type => !type.Deleted),
                    Lots = dataHandler.GetEntities().Lots.ToList().FindAll(type => !type.Deleted)
                }
            };
        }
    }
}
