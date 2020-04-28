using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class InventoryDataHandler : Interfaces.IDataHandler<Inventory>
    {
        public override void AddDataItem(Inventory dataItem)
        {
            Inventory existingItem = db.Inventories.ToList().FindAll(i => i.LotId == dataItem.LotId && i.PartId == dataItem.PartId && i.WarehouseId == dataItem.WarehouseId).FirstOrDefault();

            // If there is an existing item with this information, update it's quantity instead
            if (existingItem != null && dataItem.Quantity > 0)
            {
                existingItem.Quantity += dataItem.Quantity;
                UpdateDataItem(existingItem);
            } else
            {
                base.AddDataItem(dataItem);
            }
        }

        protected override void VerifyDataItem(Inventory dataItem)
        {
            Lot lot = db.Lots.ToList().FindAll(l => l.Id == dataItem.LotId).FirstOrDefault();
            Part part = db.Parts.ToList().FindAll(p => p.Id == dataItem.PartId).FirstOrDefault();

            // Check for valid quantity
            if (dataItem.Quantity < 1)
            {
                throw new ArgumentException("Invalid quantity");
            }

            // Check if the the lot type is different from the part type
            if (lot.Type != part.Type)
            {
                throw new ArgumentException("Lot and Part type mismatch");
            }
        }
    }
}
