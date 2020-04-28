using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class PurchaseOrderPartsDataHandler : Interfaces.IDataHandler<PurchaseOrderPart>
    {
        public override void AddDataItem(PurchaseOrderPart dataItem)
        {
            PurchaseOrderPart existingItem = db.PurchaseOrderParts.ToList().FindAll(p => p.OrderId == dataItem.OrderId && p.PartId == dataItem.PartId).FirstOrDefault();

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

        protected override void VerifyDataItem(PurchaseOrderPart dataItem)
        {
            Part part = db.Parts.ToList().FindAll(p => p.Id == dataItem.PartId).FirstOrDefault();

            // Check for valid quantity
            if (dataItem.Quantity < 1)
            {
                throw new ArgumentException("Invalid quantity");
            }

            // Verify that the part is a raw material
            if (part.Type != LotType.RawMaterial) { 
                throw new ArgumentException("Part must be raw material");
            }
        }
    }
}
