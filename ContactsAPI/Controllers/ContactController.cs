using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contacts.DataLayer;
using Contacts.DataLayer.Infrastructure;
using Contacts.DataLayer.Models;

namespace ContactsAPI.Controllers
{
    public class ContactController : ApiController
    {
        IContactsRepository _repository = null;
        public ContactController(IContactsRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        public IHttpActionResult Get()
        {
            IList<Contact> contacts = _repository.FindBy(s => (s.IsDeleted == null) ||
                                                            (s.IsDeleted.HasValue && s.IsDeleted.Value == false)
                                                            ).ToList();
            if (contacts.Count == 0)
            {
                return NotFound();
            }

            return Ok(contacts);
        }
        // GET api/contacts/5
        public IHttpActionResult Get(int id)
        {
            var contact = _repository.Get(id);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }

        // POST api/contacts
        public IHttpActionResult Post([FromBody]Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _repository.Insert(contact);            
            return CreatedAtRoute("DefaultApi", new Contact { Id = contact.Id }, contact);
        }

        // PUT api/contacts/5
        public IHttpActionResult Put(int id, [FromBody]Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _repository.Update(contact);
            return Content(HttpStatusCode.OK, contact); ;
        }

        // DELETE api/contacts/5
        public IHttpActionResult Delete(int id)
        {
            var contact = _repository.Get(id);
            _repository.Delete(contact);
            return Ok();
        }
    }
}
