using System;
using System.Collections.Generic;
using System.Text;

namespace blogModel
{
   public class PostDetail
    {
        public int Id { get; set; }

        public string Sequence { get; set; }

        public string PostText { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}
