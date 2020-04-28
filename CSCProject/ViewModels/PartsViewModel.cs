using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class PartsViewModel : DataTableViewModel<Part, DataHandlers.PartsDataHandler, Dialogs.PartDialog>
    {
        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description"), AllowSearch = true },
                new Misc.Column { Name = "Type", PropertyBinding = new Binding("Type") { Converter = new Misc.LotTypeEnumToStringConverter() }, AllowSearch = true },
                new Misc.Column { Name = "Price", PropertyBinding = new Binding("Price") { StringFormat = "{0}₪" }, AllowSearch = true },
                new Misc.Column { Name = "Unit", PropertyBinding = new Binding("Unit") { Converter = new Misc.PartUnitEnumToStringConverter() }, AllowSearch = true },
            };
        }
    }
}
