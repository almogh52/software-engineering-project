using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class LotsViewModel : DataTableViewModel<Lot, DataHandlers.LotsDataHandler, Dialogs.LotDialog>
    {
        public override bool ShowDataItemId { get; set; } = true;

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Type", PropertyBinding = new Binding("Type") { Converter = new Misc.LotTypeEnumToStringConverter() }, AllowSearch = true }
            };
        }
    }
}
