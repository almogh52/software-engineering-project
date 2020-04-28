using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class InventoryViewModel : DataTableViewModel<Inventory, DataHandlers.InventoryDataHandler, Dialogs.InventoryDialog>
    {
        public override bool HasId { get; set; } = false;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Lot", PropertyBinding = new Binding("LotId"), AllowSearch = true },
                new Misc.Column { Name = "Part Id", PropertyBinding = new Binding("PartId"), AllowSearch = true },
                new Misc.Column { Name = "Part Description", PropertyBinding = new Binding("Part.Description"), AllowSearch = true },
                new Misc.Column { Name = "Warehouse Id", PropertyBinding = new Binding("WarehouseId"), AllowSearch = true },
                new Misc.Column { Name = "Warehouse Description", PropertyBinding = new Binding("Warehouse.Description"), AllowSearch = true },
                new Misc.Column { Name = "Quantity", PropertyBinding = new Binding("Quantity"), AllowSearch = true }
            };
        }

        protected override void InitDataItem(ref Inventory dataItem)
        {
            dataItem = new Inventory
            {
                LotId = "",
                PartId = -1,
                WarehouseId = -1
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.InventoryDialog dialog, ref Inventory dataItem)
        {
            dialog = new Dialogs.InventoryDialog
            {
                DataContext = new Dialogs.InventoryDialogContext
                {
                    Inventory = dataItem,
                    Lots = dataHandler.GetEntities().Lots.ToList().FindAll(type => !type.Deleted),
                    Parts = dataHandler.GetEntities().Parts.ToList().FindAll(type => !type.Deleted),
                    Warehouses = dataHandler.GetEntities().Warehouses.ToList().FindAll(type => !type.Deleted)
                }
            };
        }
    }
}
