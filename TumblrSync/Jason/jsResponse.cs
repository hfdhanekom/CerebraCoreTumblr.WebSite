using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblrSync.Objects.Jason
{
    public class jsResponse
    {
        public jsBlog blog { get; set; }
        public List<jsPost> posts { get; set; }
        public int total_posts { get; set; }
        public string total_blogs { get; set; }
        public List<jsBlogs> blogs { get; set; }
    }
}
