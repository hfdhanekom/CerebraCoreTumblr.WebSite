using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Interface
{
    public interface ITumblrBlog
    {
        IQueryable<DTO.TumblrBlog> GetTumblrBlogs(int AccountID);
        void GetTumblrBlogCount(String AccountEmail);
        void GetTumblrBlog(String BlogName);
        void AddTumblrBlog(String BlogName);
        void DeleteTumblrBlog(String BlogName);
    }
}
