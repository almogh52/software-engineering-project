using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class SaleOrderPartsDataHandler : Interfaces.IDataHandler<SaleOrderPart>
    {
        public override void AddDataItem(SaleOrderPart dataItem)
        {
            SaleOrderPart existingItem = db.SaleOrderParts.ToList().FindAll(p => p.OrderId == dataItem.OrderId && p.PartId == dataItem.PartId && p.LotId == dataItem.LotId).FirstOrDefault();

            // If there is an existing item with this information, update it's quantity instead
            if (existingItem != null)
            {
                existingItem.Quantity += dataItem.Quantity;
                base.UpdateDataItem(existingItem);
            }
            else
            {
                base.AddDataItem(dataItem);
            }
        }

        protected override void VerifyDataItem(SaleOrderPart dataItem)
        {
            Part part = db.Parts.ToList().FindAll(p => p.Id == dataItem.PartId).FirstOrDefault();
            Lot lot = db.Lots.ToList().FindAll(l => l.Id == dataItem.LotId).FirstOrDefault();

            // Check for valid quantity
            if (dataItem.Quantity <= 0)
            {
                throw new ArgumentException("Invalid quantity");
            }

            // Verify that the part is a finished good
            if (part.Type != LotType.FinishedGood)
            {
                throw new ArgumentException("Part must be finished good");
            }

            // Verify that the lot is a finished good
            if (lot.Type != LotType.FinishedGood)
            {
                throw new ArgumentException("Lot must be finished good");
            } 
        }
    }
}
