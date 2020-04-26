using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class LotsDataHandler : Interfaces.IDataHandler<Lot>
    {
        protected override void VerifyDataItem(Lot dataItem)
        {
            // Check if the id of the lot is valid
            if (!Misc.Utils.VerifyLotId(dataItem.Id))
            {
                throw new ArgumentException("Invalid lot id");
            }
        }
    }
}
