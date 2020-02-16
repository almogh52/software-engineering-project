using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class CustomersDataHandler : Interfaces.IDataHandler<Customer>
    {
        public override void AddDataItem(Customer dataItem)
        {
            // Set the address of the customer
            dataItem.PostalCode = dataItem.Address.PostalCode;

            // Check if the address already exists in the db
            if (Misc.Utils.CheckIfAddressExists(db, dataItem.PostalCode))
            {
                // Remove address to prevent creation of new address
                dataItem.Address = null;
            }

            // Call base add data item
            base.AddDataItem(dataItem);
        }

        protected override void VerifyDataItem(Customer vendor)
        {
            // Check if the name of the customer is valid
            if (!Misc.Utils.VerifyName(vendor.Name))
            {
                throw new ArgumentException("Invalid vendor name");
            }

            // Check for valid phone number
            if (!Misc.Utils.VerifyPhone(vendor.Phone))
            {
                throw new ArgumentException("Invalid phone number");
            }

            // Check for valid address
            if (!Misc.Utils.VerifyName(vendor.Address.City))
            {
                throw new ArgumentException("Invalid city name");
            }
        }
    }
}
