
using Contacts.DataLayer.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DataLayer.Infrastructure
{
    public class ContactsRepository : Repository<Contact>, IContactsRepository
    {
        public override IQueryable<Contact> GetAll()
        {
            return base.GetAll();//.Where(s => !s.IsDeleted)
        }
        public override void Delete(Contact entity)
        {
            entity.Modified = DateTime.Now;
            entity.IsDeleted = true;
            Update(entity);
        }

        public override void Update(Contact entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }
        public override void Insert(Contact entity)
        {
            entity.Modified = DateTime.Now;
            base.Insert(entity);
        }
        public Contact GetById(int id)
        {
            return base.FindBy(s => s.Id == id).FirstOrDefault();//.Where(s => !s.IsDeleted)
        }

    }

}
