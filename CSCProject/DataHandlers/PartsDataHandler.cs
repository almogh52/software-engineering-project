using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class PartsDataHandler : Interfaces.IDataHandler<Part>
    {
        protected override void VerifyDataItem(Part dataItem)
        {
            // Check if the price of the data item is invalid
            if (dataItem.Price <= 0)
            {
                throw new ArgumentException("Invalid price");
            }
        }
    }
}
