using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class SaleOrdersDataHandler : Interfaces.IDataHandler<SaleOrder>
    {
        public override List<SaleOrder> GetData()
        {
            return base.GetData().Select(o => CalcPrice(o)).ToList();
        }

        public void ShipOrder(SaleOrder order)
        {
            InventoryDataHandler inventoryDataHandler = new InventoryDataHandler();

            List<Inventory> inventory = inventoryDataHandler.GetData();

            foreach (var part in order.Parts)
            {
                if (!part.Deleted)
                {
                    Inventory partInventory = inventory.Find(i => i.PartId == part.PartId && i.LotId == part.LotId);

                    // If the part is missing or insufficient in the inventory
                    if (partInventory == null || partInventory.Quantity < part.Quantity)
                    {
                        throw new ArgumentException($"Insufficient goods\nMissing '{(partInventory == null ? part.Quantity : part.Quantity - partInventory.Quantity)} {part.Part.Unit}' of Part '{part.Part.Description}'");
                    }

                    if (partInventory.Quantity == part.Quantity)
                    {
                        inventoryDataHandler.RemoveDataItem(partInventory);
                    }
                    else
                    {
                        partInventory.Quantity -= part.Quantity;
                        inventoryDataHandler.UpdateDataItem(partInventory);
                    }
                }
            }

            // Set the order as shipped
            order.Shipped = true;
            UpdateDataItem(order);
        }

        private static SaleOrder CalcPrice(SaleOrder order)
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

        protected override void VerifyDataItem(SaleOrder dataItem)
        {
        }
    }
}
