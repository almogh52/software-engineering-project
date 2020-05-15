using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class PurchaseOrdersDataHandler : Interfaces.IDataHandler<PurchaseOrder>
    {
        public override List<PurchaseOrder> GetData()
        {
            return base.GetData().Select(o => CalcPrice(o)).ToList();
        }

        public void RecieveOrder(PurchaseOrder order, int warehouseId)
        {
            InventoryDataHandler inventoryDataHandler = new InventoryDataHandler();

            if (db.Warehouses.Find(warehouseId) == null)
            {
                throw new ArgumentException("Warehouse not found");
            }

            foreach (var part in order.Parts)
            {
                if (!part.Deleted)
                {
                    inventoryDataHandler.AddDataItem(new Inventory { LotId = part.LotId, PartId = part.PartId, Quantity = part.Quantity, WarehouseId = warehouseId });
                }
            }

            // Set the order as recieved
            order.Received = true;
            UpdateDataItem(order);
        }

        private static PurchaseOrder CalcPrice(PurchaseOrder order)
        {
            float price = 0;

            // Reload parts collection
            db.Entry(order).Collection(o => o.Parts).Load();

            foreach (var part in order.Parts)
            {
                ((IObjectContextAdapter)db).ObjectContext.Refresh(RefreshMode.StoreWins, part);
                if (!part.Deleted)
                {
                    price += part.Part.Price * part.Quantity;
                }
            }

            order.Price = price;

            return order;
        }

        protected override void VerifyDataItem(PurchaseOrder dataItem)
        {
        }
    }
}
