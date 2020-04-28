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
            SaleOrderPart existingItem = db.SaleOrderParts.ToList().FindAll(p => p.OrderId == dataItem.OrderId && p.PartId == dataItem.PartId).FirstOrDefault();

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

        protected override void VerifyDataItem(SaleOrderPart dataItem)
        {
            // Check for valid quantity
            if (dataItem.Quantity < 1)
            {
                throw new ArgumentException("Invalid quantity");
            }
        }
    }
}
