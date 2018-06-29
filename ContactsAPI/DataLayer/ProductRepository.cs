using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.DataLayer
{
    public class ContactRepository : IDisposable
    {
        private ContactsContext db = new ContactsContext();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public IEnumerable<Contact> GetAll()
        {
            return db.Contact;
        }
        public Product GetByID(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
    }


}
