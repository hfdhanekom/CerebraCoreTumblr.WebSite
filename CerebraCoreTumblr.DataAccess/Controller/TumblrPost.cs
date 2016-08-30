using CerebraCoreTumblr.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Controller
{
    public class TumblrPost : ITumblrPost
    {
        public IQueryable<DTO.TumblrPost> GetTumblrPosts(string BlogName)
        {
            throw new NotImplementedException();
        }

        public void GetTumblrPost(int PostId)
        {
            throw new NotImplementedException();
        }

        public void AddTumblrPost(string Json)
        {
            throw new NotImplementedException();
        }

        public void DeleteTumblrAccount(int PostId)
        {
            throw new NotImplementedException();
        }

        public DTO.TumblrPost GetPost(string LinkID)
        {
            throw new NotImplementedException();
        }
    }
}
