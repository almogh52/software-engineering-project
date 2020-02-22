using Caliburn.Micro;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.ViewModels
{
    class ExpensesDistributionViewModel : Screen, INotifyPropertyChanged
    {
        public PlotModel Model {
            get
            {
                DataHandlers.ExpensesDataHandler dataHandler = new DataHandlers.ExpensesDataHandler();

                PlotModel model = new PlotModel();
                dynamic pieSeries = new PieSeries { InsideLabelPosition = 0.7, OutsideLabelFormat = "{0}₪ - {2:0.#}%", Font = "Roboto" };

                List<Expense> expenses = dataHandler.GetData().FindAll(expense => !expense.Deleted);
                Dictionary<Employee, int> employeesExpenses = new Dictionary<Employee, int>();

                // For each expense, add it to the employee's total expense
                foreach (Expense expense in expenses)
                {
                    // Check if the employee key exists
                    if (!employeesExpenses.ContainsKey(expense.Employee))
                    {
                        employeesExpenses[expense.Employee] = 0;
                    }

                    // Add the expense price to the employee's expense
                    employeesExpenses[expense.Employee] += expense.Price;
                }

                // For each employee, create it's pie slice
                foreach (var employeeTotalExpense in employeesExpenses)
                {
                    pieSeries.Slices.Add(new PieSlice(string.Format("{0} {1}", employeeTotalExpense.Key.FirstName, employeeTotalExpense.Key.LastName), employeeTotalExpense.Value));
                }

                // Add the pie series to the model1
                model.Series.Add(pieSeries);

                return model;
            }
        }
    }
}
