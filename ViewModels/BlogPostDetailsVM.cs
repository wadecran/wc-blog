using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wc_Blog.Controllers;
using wc_Blog.Models;

namespace wc_Blog.ViewModels
{
    public class BlogPostDetailsVM
    {
        public BlogPost BlogPost { get; set; }
        public ICollection<BlogPost> BottomPosts { get; set; }

        public BlogPostDetailsVM()
        {
            BottomPosts = new HashSet<BlogPost>();
        }
    }
}