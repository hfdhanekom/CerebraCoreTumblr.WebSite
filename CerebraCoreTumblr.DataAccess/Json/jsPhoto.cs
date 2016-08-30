using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Jason
{
    public class jsPhoto
    {
        public string caption { get; set; }
        public List<jsAltSize> alt_sizes { get; set; }
        public jsOriginalSize original_size { get; set; }
        public jsExif exif { get; set; }
    }
}
