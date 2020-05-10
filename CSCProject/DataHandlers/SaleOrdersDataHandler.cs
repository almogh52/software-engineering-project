using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class SaleOrdersDataHandler : Interfaces.IDataHandler<SaleOrder>
    {
        public override List<SaleOrder> GetData()
        {
            return base.GetData().Select(o => CalcPrice(o)).ToList();
        }

        protected override void VerifyDataItem(SaleOrder dataItem)
        {
        }

        private static SaleOrder CalcPrice(SaleOrder order)
        {
            float price = 0;

            // Reload parts collection
            db.Entry(order).Collection(o => o.Parts).Load();

            foreach (var part in order.Parts)
            {
                ((IObjectContextAdapter)db).ObjectContext.Refresh(RefreshMode.StoreWins, part);
                if (!part.Deleted)
                {
                    price += part.Part.Price * part.Quantity;
                }
            }

            order.Price = price;

            return order;
        }
    }
}
