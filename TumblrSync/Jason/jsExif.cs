using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblrSync.Objects.Jason
{
    public class jsExif
    {
        public string Camera { get; set; }
        public int ISO { get; set; }
        public string Aperture { get; set; }
        public string Exposure { get; set; }
        public string FocalLength { get; set; }
    }
}
