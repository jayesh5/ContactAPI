using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.DataLayer
{
    public class ContactsContext : DbContext
    {
        public ProductsContext()
       : base("name=ProductsContext")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}