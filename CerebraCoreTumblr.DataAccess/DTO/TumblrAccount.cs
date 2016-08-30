using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.DTO
{
    public class TumblrAccount
    {
        //Data
        public int id { get; set; }
        public string tb_username { get; set; }
        public string tb_Password { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        //Website and Download 
        public string systemDrive { get; set; }
        public string AccountSystemPath { get; set; }
        public string AccountWebPath { get; set; }
    }
}
