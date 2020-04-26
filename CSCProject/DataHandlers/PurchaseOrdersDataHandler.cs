using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class PurchaseOrdersDataHandler : Interfaces.IDataHandler<PurchaseOrder>
    {
        protected override void VerifyDataItem(PurchaseOrder dataItem)
        {
        }
    }
}
