using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblrSync.Objects.Jason
{
    public class jsReblog
    {
        public string tree_html { get; set; }
        public string comment { get; set; }
        public List<object> trail { get; set; }
    }
}
