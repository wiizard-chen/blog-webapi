using System;
using System.Collections.Generic;
using System.Text;

namespace blogModel
{
   public class Comment
    {
        public int Id { get; set; }

        public string Comments { get; set; }

        public DateTime CommnetDate { get; set; }

        public int Status { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
