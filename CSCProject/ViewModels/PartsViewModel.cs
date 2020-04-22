﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class PartsViewModel : DataTableViewModel<Part, DataHandlers.PartDataHandler, Dialogs.PartDialog>
    {
        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description"), AllowSearch = true },
                new Misc.Column { Name = "Type", PropertyBinding = new Binding("Type") { Converter = new Misc.LotTypeEnumToStringConverter() }, AllowSearch = true },
                new Misc.Column { Name = "Purchase Price", PropertyBinding = new Binding("PurchasePrice"), AllowSearch = true },
                new Misc.Column { Name = "Unit", PropertyBinding = new Binding("Unit") { Converter = new Misc.PartUnitEnumToStringConverter() }, AllowSearch = true },
            };
        }
    }
}