using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class SaleOrdersDataHandler : Interfaces.IDataHandler<SaleOrder>
    {
        protected override void VerifyDataItem(SaleOrder dataItem)
        {
        }
    }
}
