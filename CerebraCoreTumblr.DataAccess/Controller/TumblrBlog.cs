using CerebraCoreTumblr.DataAccess.Interface;
using CerebraCoreTumblr.DataAccess.Jason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Controller
{
    public class TumblrBlog : ITumblrBlog
    {


        public IQueryable<DTO.TumblrBlog> GetTumblrBlogs(string AccountEmail)
        {
            throw new NotImplementedException();
        }

        public void GetTumblrBlogCount(string AccountEmail)
        {
            throw new NotImplementedException();
        }

        public void GetTumblrBlog(string BlogName)
        {
            throw new NotImplementedException();
        }

        public void AddTumblrBlog(string BlogName)
        {
            throw new NotImplementedException();
        }

        public void DeleteTumblrBlog(string BlogName)
        {
            throw new NotImplementedException();
        }
    }
}
