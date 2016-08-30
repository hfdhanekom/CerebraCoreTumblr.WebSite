using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.DTO
{
    public class TumblrPost
    {
        #region Application Req

        public string Systempath { get; set; }
        public string Webpath { get; set; }
        public bool Downloaded { get; set; }
        public string Filename { get; set; }

        #endregion


    }
}
