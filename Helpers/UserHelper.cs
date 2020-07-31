using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wc_Blog.Models;

namespace wc_Blog.Helpers
{
    public class UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string GetDisplayName(string userId)
        {
            var user = db.Users.Find(userId);
            return user.DisplayName;
        }

        public string GetUserEmail(string userId)
        {
            return db.Users.Find(userId).UserName;
        }

        public string GetFullName(string userId)
        {
            var user = db.Users.Find(userId); ;
            var firstName = user.FirstName;
            var lastName = user.LastName;
            return firstName + " " + lastName;
        }
    }
}