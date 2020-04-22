using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class PartDataHandler : Interfaces.IDataHandler<Part>
    {
        protected override void VerifyDataItem(Part dataItem)
        {
            // Check if the purchase price of the data item is invalid
            if (dataItem.PurchasePrice < 0 || (dataItem.Type == LotType.RawMaterial && dataItem.PurchasePrice == 0) || (dataItem.Type == LotType.FinishedGood && dataItem.PurchasePrice > 0))
            {
                throw new ArgumentException("Invalid purchase price");
            }
        }
    }
}
