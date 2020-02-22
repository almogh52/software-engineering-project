using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSCProject.DataHandlers
{
    class VendorsDataHandler : Interfaces.IDataHandler<Vendor>
    {
        public override void AddDataItem(Vendor dataItem)
        {
            // Set the address of the vendor
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

        protected override void VerifyDataItem(Vendor dataItem)
        {
            Address address = dataItem.Address == null ? db.Addresses.Find(dataItem.PostalCode) : dataItem.Address;

            // Check if the name of the customer is valid
            if (!Misc.Utils.VerifyName(dataItem.Name))
            {
                throw new ArgumentException("Invalid vendor name");
            }

            // Check for valid phone number
            if (!Misc.Utils.VerifyPhone(dataItem.Phone))
            {
                throw new ArgumentException("Invalid phone number");
            }

            // Check for valid address
            if (!Misc.Utils.VerifyName(address.City))
            {
                throw new ArgumentException("Invalid city name");
            }
        }
    }
}
