using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.ViewModels
{
    class OrdersViewModel<T, U, T1, T2, T3, U1, U2, U3> : DualDataTableViewModel<T, U> 
        where T : DataTableViewModel<T1, T2, T3>, new() where U : DataTableViewModel<U1, U2, U3>, new()
        where T1 : class, new() where T2 : Interfaces.IDataHandler<T1>, new() where T3 : Dialogs.Dialog, new()
        where U1 : class, new() where U2 : Interfaces.IDataHandler<U1>, new() where U3 : Dialogs.Dialog, new()
    {
        public override string SecondViewName { get; set; } = "Order Parts:";

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            FirstViewModel.ItemSelected += FirstViewModel_ItemSelected;
        }

        private void FirstViewModel_ItemSelected(object sender, DataItemSelectedEventArgs<T1> e)
        {
            SecondViewModel.SearchColumnIndex = 0; // This should be the order id column
            SecondViewModel.SearchText = e.DataItem.GetType().GetProperty("Id").GetValue(e.DataItem).ToString();
        }
    }

    class PurchaseOrdersCombinedViewModel : OrdersViewModel<PurchaseOrdersViewModel, PurchaseOrderPartsViewModel, PurchaseOrder, DataHandlers.PurchaseOrdersDataHandler, Dialogs.PurchaseOrderDialog, PurchaseOrderPart, DataHandlers.PurchaseOrderPartsDataHandler, Dialogs.PurchaseOrderPartDialog> { }
    class SaleOrdersCombinedViewModel : OrdersViewModel<SaleOrdersViewModel, SaleOrderPartsViewModel, SaleOrder, DataHandlers.SaleOrdersDataHandler, Dialogs.SaleOrderDialog, SaleOrderPart, DataHandlers.SaleOrderPartsDataHandler, Dialogs.SaleOrderPartDialog> { }
}
