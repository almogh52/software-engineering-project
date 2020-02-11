using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCProject.Interfaces
{
    abstract class IDataHandler<T> where T : class
    {
        protected dbEntities db = new dbEntities();

        public virtual List<T> GetData()
        {
            return db.Set<T>().ToList();
        }

        public virtual void RemoveDataItem(T dataItem)
        {
            // Set the data item as deleted
            typeof(T).GetProperty("Deleted").SetValue(dataItem, true);

            // Set the entity as changed
            db.Entry(dataItem).State = System.Data.Entity.EntityState.Modified;

            // Save the database
            db.SaveChanges();
        }

        public virtual void AddDataItem(T dataItem)
        {
            // Verify the data item's content
            VerifyDataItem(dataItem);

            // Add the data item
            db.Set<T>().Add(dataItem);

            // Save changes to the database
            db.SaveChanges();
        }

        public dbEntities GetEntities()
        {
            return db;
        }

        protected abstract void VerifyDataItem(T dataItem);
    }
}
