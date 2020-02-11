﻿using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    abstract class DataTableViewModel<T, U, V> : Screen, INotifyPropertyChanged where T : class, new() where U : Interfaces.IDataHandler<T>, new() where V : UserControl, new()
    {
        protected Interfaces.IDataHandler<T> dataHandler = new U();

        public List<T> Data { get => dataHandler.GetData(); }

        public string DataItemName { get; } = typeof(T).Name;

        public virtual string AddButtonIcon { get; set; } = "Add";
        public virtual string RemoveButtonIcon { get; set; } = "Remove";

        public void InitDataGrid()
        {
            DataGrid dataGrid = (GetView() as Views.DataTableView).Data;

            // Add each column to the data grid
            foreach (var column in GetColumns())
            {
                // Check for checkbox column
                if (Misc.Utils.FollowPropertyPath(typeof(T), column.PropertyBinding.Path.Path) == typeof(bool))
                {
                    dataGrid.Columns.Add(new DataGridCheckBoxColumn { Header = column.Name, Binding = column.PropertyBinding, ElementStyle = Application.Current.Resources["MaterialDesignDataGridCheckBoxColumnStyle"] as Style });
                } else
                {
                    dataGrid.Columns.Add(new DataGridTextColumn { Header = column.Name, Binding = column.PropertyBinding });
                }
            }
        }

        public bool CanRemoveDataItem(T dataItem)
        {
            // Check if the data item is deleted
            return dataItem != null && (bool)typeof(T).GetProperty("Deleted").GetValue(dataItem) == false;
        }

        public void RemoveDataItem(T dataItem)
        {
            // Remove the data item from the data set
            dataHandler.RemoveDataItem(dataItem);

            // Update the table
            UpdateTable();
        }

        public async void ShowNewDataItemDialog()
        {
            T dataItem = null;
            V newDataItemDialog = null;

            // Initialize the new data item dialog and the new item object
            InitNewDataItemDialog(ref newDataItemDialog, ref dataItem);

            // Show the new data item dialog
            await DialogHost.Show(newDataItemDialog, (object _, DialogClosingEventArgs args) =>
            {
                // Check if the dialog closing is a message dialog
                if (args.Session.Content.GetType() == typeof(Dialogs.MessageDialog))
                {
                    // Cancel the close of the dialog
                    args.Cancel();

                    // Show the new data item dialog again
                    args.Session.UpdateContent(newDataItemDialog);
                }
                else
                {
                    // If not cancelled
                    if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                    {
                        try
                        {
                            // Add the data item
                            AddDataItem(dataItem);
                        }
                        catch (Exception ex)
                        {
                            // Cancel the close of the dialog
                            args.Cancel();

                            // Show the error
                            args.Session.UpdateContent(new Dialogs.MessageDialog { Message = ex.Message });
                        }

                        // Update the data table
                        UpdateTable();
                    }
                }
            });
        }

        public void AddDataItem(T dataItem)
        {
            // Add the data item to the data set
            dataHandler.AddDataItem(dataItem);

            // Update the table
            UpdateTable();
        }

        private void UpdateTable()
        {
            NotifyOfPropertyChange("Data");
        }

        protected abstract void InitNewDataItemDialog(ref V dialog, ref T dataItem);
        protected abstract List<Misc.Column> GetColumns();
    }
}