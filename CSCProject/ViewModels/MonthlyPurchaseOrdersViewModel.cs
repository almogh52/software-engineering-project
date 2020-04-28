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
    class MonthlyPurchaseOrdersViewModel : Screen, INotifyPropertyChanged
    {
        public PlotModel Model
        {
            get
            {
                DataHandlers.PurchaseOrdersDataHandler dataHandler = new DataHandlers.PurchaseOrdersDataHandler();

                PlotModel model = new PlotModel();
                ColumnSeries columnSeries = new ColumnSeries { Font = "Roboto", LabelPlacement = LabelPlacement.Inside, LabelFormatString = "{0}" };

                List<PurchaseOrder> purchaseOrders = dataHandler.GetData().FindAll(order => !order.Deleted);
                int[] monthlyOrders = new int[12];

                // For each purchase order, check if it's in the last year
                foreach (PurchaseOrder purchaseOrder in purchaseOrders)
                {
                    if (purchaseOrder.Date.Year == DateTime.Today.Year)
                    {
                        monthlyOrders[purchaseOrder.Date.Month - 1]++;
                    }
                }

                // For each month, add it's value to the column series items
                for (int month = 0; month < 12; month++)
                {
                    columnSeries.Items.Add(new ColumnItem { Value = monthlyOrders[month], Color = OxyColor.FromRgb(255,165,0) });
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
                    AbsoluteMinimum = 0
                });

                return model;
            }
        }
    }
}
