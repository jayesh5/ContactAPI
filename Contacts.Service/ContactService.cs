using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.DataLayer.Infrastructure;
using Contacts.DataLayer.Models;

namespace Contacts.Service
{
    public class ContactService : IContactService
    {
        public ContactService(IUnitOfWork dal) {

        }
        public void DeleteContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Contact> GetContacts()
        {
            throw new NotImplementedException();
        }

        public void InsertContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
