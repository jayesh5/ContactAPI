using Contacts.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DataLayer.Infrastructure
{
    public interface IContactsRepository : IRepository<Contact>
    {
        
    }
}
