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
    class SoldGoodsViewModel : Screen, INotifyPropertyChanged
    {
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime EndDate { get; set; } = DateTime.Now;

        public PlotModel Model
        {
            get
            {
                DataHandlers.SaleOrderPartsDataHandler dataHandler = new DataHandlers.SaleOrderPartsDataHandler();

                PlotModel model = new PlotModel();

                List<SaleOrderPart> saleOrderParts = dataHandler.GetData().FindAll(part => !part.Deleted);
                List<BarItem> barItems = new List<BarItem>();
                Dictionary<Part, float> soldGoods = new Dictionary<Part, float>();

                // For each sale order part, add it to the part's total sale price
                foreach (SaleOrderPart saleOrderPart in saleOrderParts)
                {
                    if (saleOrderPart.Order.Date.Date >= StartDate.Date && saleOrderPart.Order.Date.Date <= EndDate.Date)
                    {
                        // Check if the part key exists
                        if (!soldGoods.ContainsKey(saleOrderPart.Part))
                        {
                            soldGoods[saleOrderPart.Part] = 0;
                        }

                        // Add the sale price to the part's price
                        soldGoods[saleOrderPart.Part] += saleOrderPart.Part.Price * saleOrderPart.Quantity;
                    }
                }

                // For each sold good, add it to the items source
                foreach (var soldGood in soldGoods)
                {
                    barItems.Add(new BarItem { Value = soldGood.Value, Color = OxyColor.FromRgb(244, 67, 54) });
                }

                // Add the bar series to the model1
                model.Series.Add(new BarSeries
                {
                    Font = "Roboto",
                    ItemsSource = barItems,
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0:.000}₪"
                });

                // Add axes
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Left,
                    Key = "CakeAxis",
                    ItemsSource = soldGoods.Keys.ToList().Select(p => p.Description)
                });

                return model;
            }
        }
    }

    class ExtendedSoldGoodsViewModel : SoldGoodsViewModel { }
}
