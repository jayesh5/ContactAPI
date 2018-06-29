using Contacts.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Contacts.DataLayer.Infrastructure
{
    public class ContactsContext : DbContext
    {
        public ContactsContext()
       : base("name=DBContext")
        {
            Database.SetInitializer<ContactsContext>(new CreateDatabaseIfNotExists<ContactsContext>());
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}