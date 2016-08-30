using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Interface
{
    public interface ITumblrPost
    {
        IQueryable<DTO.TumblrPost> GetTumblrPosts(String BlogName);

        void GetTumblrPost(int PostId);
        void AddTumblrPost(String Json);
        void DeleteTumblrAccount(int PostId);

        public DTO.TumblrPost GetPost(String LinkID);
    }
}
