using System;
using System.Collections.Generic;
using System.Text;

namespace blogModel
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PostDate { get; set; }

        public int Status { get; set; }

        public List<PostDetail> PostDetails { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
