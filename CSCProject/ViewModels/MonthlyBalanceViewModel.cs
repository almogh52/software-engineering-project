using Caliburn.Micro;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.ViewModels
{
    class MonthlyBalanceViewModel : Screen, INotifyPropertyChanged
    {
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime EndDate { get; set; } = DateTime.Now;

        public PlotModel Model
        {
            get
            {
                DataHandlers.ExpensesDataHandler expensesDataHandler = new DataHandlers.ExpensesDataHandler();
                DataHandlers.SaleOrdersDataHandler saleOrdersDataHandler = new DataHandlers.SaleOrdersDataHandler();
                DataHandlers.PurchaseOrdersDataHandler purchaseOrdersDataHandler = new DataHandlers.PurchaseOrdersDataHandler();

                PlotModel model = new PlotModel();
                ColumnSeries columnSeries = new ColumnSeries { Font = "Roboto", LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}₪" };

                List<PurchaseOrder> purchaseOrders = purchaseOrdersDataHandler.GetData().FindAll(order => !order.Deleted);
                List<SaleOrder> saleOrders = saleOrdersDataHandler.GetData().FindAll(order => !order.Deleted);
                List<Expense> expenses = expensesDataHandler.GetData().FindAll(expense => !expense.Deleted);

                float[] balance = new float[12];

                // For each purchase order, check if it's in the last year
                foreach (PurchaseOrder purchaseOrder in purchaseOrders)
                {
                    if (purchaseOrder.Date.Date >= StartDate.Date && purchaseOrder.Date.Date <= EndDate.Date)
                    {
                        balance[purchaseOrder.Date.Month - 1] -= purchaseOrder.Price;
                    }
                }

                // For each expense, check if it's in the last year
                foreach (Expense expense in expenses)
                {
                    if (expense.Date.Date >= StartDate.Date && expense.Date.Date <= EndDate.Date)
                    {
                        balance[expense.Date.Month - 1] -= expense.Price;
                    }
                }

                // For each sale order, check if it's in the last year
                foreach (SaleOrder saleOrder in saleOrders)
                {
                    if (saleOrder.Date.Date >= StartDate.Date && saleOrder.Date.Date <= EndDate.Date)
                    {
                        balance[saleOrder.Date.Month - 1] += saleOrder.Price;
                    }
                }

                // For each month, add it's value to the column series items
                for (int month = 0; month < 12; month++)
                {
                    columnSeries.Items.Add(new ColumnItem { Value = balance[month], Color = balance[month] > 0 ? OxyColor.FromRgb(0, 230, 118) : OxyColor.FromRgb(255, 23, 68) });
                }

                // Add the column series to the model1
                model.Series.Add(columnSeries);

                // Add axes
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "CakeAxis",
                    ItemsSource = new[] {
                        "January",
                        "Feburary",
                        "March",
                        "April",
                        "May",
                        "June",
                        "July",
                        "August",
                        "September",
                        "October",
                        "November",
                        "December"
                    }
                });
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    ExtraGridlines = new Double[] { 0 }
                });

                return model;
            }
        }
    }
    
    class ExtendedMonthlyBalanceViewModel : MonthlyBalanceViewModel { }
}
