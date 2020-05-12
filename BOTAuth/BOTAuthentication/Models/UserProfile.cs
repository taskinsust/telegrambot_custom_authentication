using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContactNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int UserStatus { get; set; }
        public string CreationDate { get; set; }
    }
}
