using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.DataLayer.Models;
namespace Contacts.Service
{
    public interface IContactService
    {
        IQueryable<Contact> GetContacts();
        Contact GetContact(long id);
        void InsertContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
