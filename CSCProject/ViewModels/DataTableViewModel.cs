﻿using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CSCProject.ViewModels
{
    public class DataItemSelectedEventArgs<T> : EventArgs
    {
        public T DataItem { get; set; }
    }

    abstract class DataTableViewModel<T, U, V> : Screen, INotifyPropertyChanged where T : class, new() where U : Interfaces.IDataHandler<T>, new() where V : Dialogs.Dialog, new()
    {
        protected Interfaces.IDataHandler<T> dataHandler = new U();

        public string DataItemName { get; } = Regex.Replace(typeof(T).Name, "(\\B[A-Z])", " $1");

        public virtual bool HasId { get; set; } = true;
        public virtual bool ShowDataItemId { get; set; } = false;
        public bool ShowDeletedItems { get; set; } = false;

        public List<Misc.Column> SearchableColumns { get => GetColumns().FindAll(column => column.AllowSearch); }
        public int SearchColumnIndex { get; set; } = -1;
        public string SearchText { get; set; }

        public event EventHandler<DataItemSelectedEventArgs<T>> ItemSelected;
        public event EventHandler DataUpdated;

        public List<T> Data
        {
            get
            {
                List<T> OriginalData = ShowDeletedItems ? dataHandler.GetData() : dataHandler.GetData().FindAll(item => (bool)typeof(T).GetProperty("Deleted").GetValue(item) == false);

                // If no search text or column, return original data
                if (SearchText == null || SearchColumnIndex == -1 || SearchText.Length == 0)
                {
                    return OriginalData;
                }
                else
                {
                    Misc.Column searchColumn = SearchableColumns[SearchColumnIndex];
                    return OriginalData.FindAll(item => Misc.Utils.GetPropertyValue(typeof(T), searchColumn.PropertyBinding is Binding ? ((Binding)searchColumn.PropertyBinding).Path.Path : ((Binding)((MultiBinding)searchColumn.PropertyBinding).Bindings[0]).Path.Path, item).ToString().Contains(SearchText));
                }
            }
        }

        public virtual string AddButtonIcon { get; set; } = "Add";
        public virtual string RemoveButtonIcon { get; set; } = "Remove";

        public virtual bool HasDataContextMenuExtraButton { get; } = false;
        public virtual string DataContextMenuExtraButtonText { get; }
        public virtual string DataContextMenuExtraButtonIcon { get; }
        public virtual bool DataContextMenuExtraButtonEnabled { get; } = true;

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            DataGrid dataGrid = (view as Views.DataTableView).Data;

            // Add each column to the data grid
            foreach (var column in GetColumns())
            {
                // Check for checkbox column
                if (column.PropertyBinding is Binding && Misc.Utils.FollowPropertyPath(typeof(T), ((Binding)column.PropertyBinding).Path.Path) == typeof(bool))
                {
                    // Insert before the deleted field
                    dataGrid.Columns.Insert(dataGrid.Columns.Count - 1, new DataGridCheckBoxColumn { Header = column.Name, Binding = column.PropertyBinding, ElementStyle = Application.Current.Resources["MaterialDesignDataGridCheckBoxColumnStyle"] as Style });
                }
                else
                {
                    // Insert before the deleted field
                    dataGrid.Columns.Insert(dataGrid.Columns.Count - 1, new System.Windows.Controls.DataGridTextColumn { Header = column.Name, Binding = column.PropertyBinding });
                }
            }
        }

        public bool CanChangeDataItemDeleteStatus(T dataItem)
        {
            // Check if the data item isn't null
            return dataItem != null;
        }

        public void ChangeDataItemDeleteStatus(T dataItem)
        {
            // If the item isn't deleted yet
            if ((bool)typeof(T).GetProperty("Deleted").GetValue(dataItem) == false)
            {
                // Remove the data item from the data set
                dataHandler.RemoveDataItem(dataItem);
            } else
            {
                // Restore the data item to the data set
                dataHandler.RestoreDataItem(dataItem);
            }

            // Update the table
            UpdateTable();
        }

        public bool CanEditDataItem(T dataItem)
        {
            // Check if the data item isn't null
            return dataItem != null;
        }
        
        public void EditDataItem(T dataItem)
        {
            // Show the data item dialog of the item
            ShowDataItemDialog(dataItem);
        }

        public async void ShowDataItemDialog(T dataItem)
        {
            bool update = dataItem != null;

            V dataItemDialog = null;

            // If the dialog isn't for updating, init the data item
            if (!update) {
                InitDataItem(ref dataItem);
            }

            // Initialize the data item dialog and the new item object
            InitDataItemDialog(ref dataItemDialog, ref dataItem);

            // Set the dialog as update dialog if needed
            dataItemDialog.UpdateDialog = update;

            // Show the data item dialog
            await DialogHost.Show(dataItemDialog, (object _, DialogClosingEventArgs args) =>
            {
                // Check if the dialog closing is a message dialog
                if (args.Session.Content.GetType() == typeof(Dialogs.MessageDialog))
                {
                    // Cancel the close of the dialog
                    args.Cancel();

                    // Show the data item dialog again
                    args.Session.UpdateContent(dataItemDialog);
                }
                else
                {
                    // If not cancelled
                    if (args.Parameter.GetType() == typeof(bool) && (bool)args.Parameter)
                    {
                        try
                        {
                            if (update)
                            {
                                // Update the data item
                                UpdateDataItem(dataItem);
                            } else
                            {
                                // Add the data item
                                AddDataItem(dataItem);
                            }
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
                    } else
                    {
                        // Refresh the entity in case it changed
                        ((IObjectContextAdapter)dataHandler.GetEntities()).ObjectContext.RefreshAsync(RefreshMode.StoreWins, dataItem);

                        // Update the data table
                        UpdateTable();
                    }
                }
            });
        }

        public void ClearSearch()
        {
            SearchText = null;
            SearchColumnIndex = -1;
        }

        public void DataSelectionChanged()
        {
            EventHandler<DataItemSelectedEventArgs<T>> handler = ItemSelected;
            handler?.Invoke(this, new DataItemSelectedEventArgs<T>
            {
                DataItem = (T)(GetView() as Views.DataTableView).Data.SelectedItem
            });
            NotifyOfPropertyChange("DataContextMenuExtraButtonEnabled");
        }

        public virtual void DataExtraButtonClicked(T dataItem)
        {
        }

        private void AddDataItem(T dataItem)
        {
            // Add the data item to the data set
            dataHandler.AddDataItem(dataItem);

            // Update the table
            UpdateTable();
        }

        private void UpdateDataItem(T dataItem)
        {
            // Update the data item
            dataHandler.UpdateDataItem(dataItem);

            // Update the table
            UpdateTable();
        }

        protected void UpdateTable()
        {
            NotifyOfPropertyChange("Data");
            DataUpdated?.Invoke(this, null);
        }

        protected virtual void InitDataItem(ref T dataItem)
        {
            dataItem = Misc.Utils.Create<T>();
        }

        protected virtual void InitDataItemDialog(ref V dialog, ref T dataItem)
        {
            dialog = new V
            {
                DataContext = dataItem
            };
        }

        protected abstract List<Misc.Column> GetColumns();
    }
}
