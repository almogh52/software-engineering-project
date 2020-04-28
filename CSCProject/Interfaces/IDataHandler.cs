using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.Interfaces
{
    abstract class IDataHandler<T> where T : class
    {
        static protected dbEntities db = new dbEntities();

        public virtual List<T> GetData()
        {
            return db.Set<T>().ToList();
        }

        public virtual void RemoveDataItem(T dataItem)
        {
            // Set the data item as deleted
            typeof(T).GetProperty("Deleted").SetValue(dataItem, true);

            // Update the data item
            UpdateDataItem(dataItem);
        }

        public virtual void RestoreDataItem(T dataItem)
        {
            // Set the data item as not deleted
            typeof(T).GetProperty("Deleted").SetValue(dataItem, false);

            // Update the data item
            UpdateDataItem(dataItem);
        }

        public virtual void AddDataItem(T dataItem)
        {
            // Verify the data item's content
            VerifyDataItem(dataItem);

            // Add the data item
            try
            {
                db.Set<T>().Add(dataItem);
            }
            catch
            {
                throw new ArgumentException("Data item already exists in table!");
            }

            // Save changes to the database
            db.SaveChanges();
        }

        public virtual void UpdateDataItem(T dataItem)
        {
            // Verify the data item's content
            VerifyDataItem(dataItem);

            // Update the entity
            try 
            {
                db.Set<T>().AddOrUpdate(dataItem);
            }
            catch
            {
                throw new ArgumentException("Data item already exists in table!");
            }

            // Save the database
            db.SaveChanges();
        }

        public dbEntities GetEntities()
        {
            // Reload entities
            db = new dbEntities();
            return db;
        }

        protected abstract void VerifyDataItem(T dataItem);
    }
}
