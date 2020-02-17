using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    class WarehousesViewModel : DataTableViewModel<Warehouse, DataHandlers.WarehousesDataHandler, Dialogs.WarehouseDialog>
    {
        public override string AddButtonIcon { get; set; } = "TableColumnAddAfter";
        public override string RemoveButtonIcon { get; set; } = "TableColumnRemoveAfter";

        protected override List<Misc.Column> GetColumns()
        {
            return new List<Misc.Column> {
                new Misc.Column { Name = "Description", PropertyBinding = new Binding("Description"), AllowSearch = true }
            };
        }

        protected override void InitDataItemDialog(ref Dialogs.WarehouseDialog dialog, ref Warehouse dataItem)
        {
            dialog = new Dialogs.WarehouseDialog
            {
                DataContext = dataItem
            };
        }
    }
}
