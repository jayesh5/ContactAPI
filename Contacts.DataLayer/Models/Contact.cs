using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contacts.DataLayer.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [RegularExpression("([0-9]+)")]
        public string PhoneNumber { get; set; }
        public bool? Status { set; get; }
        public DateTime? Modified { set; get; }
        public bool? IsDeleted { set; get; }
    }
}