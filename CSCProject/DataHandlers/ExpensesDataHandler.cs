using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class ExpensesDataHandler : Interfaces.IDataHandler<Expense>
    {
        protected override void VerifyDataItem(Expense dataItem)
        {
            // Check fi the price of the expense is a valid price
            if (dataItem.Price <= 0)
            {
                throw new ArgumentException("Invalid expense price");
            }
        }
    }
}
