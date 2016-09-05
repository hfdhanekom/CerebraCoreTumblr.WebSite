using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblrSync.Objects.Jason
{
    public class jsTrail
    {
        public jsBlog2 blog { get; set; }
        public jsPost2 post { get; set; }
        public string content_raw { get; set; }
        public string content { get; set; }
        public bool is_current_item { get; set; }
        public bool is_root_item { get; set; }

    }
}
