using System;
using System.Collections.Generic;

namespace blogModel
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UserMail { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        public List<Role> Roles { get; set; }
    }
}
