using CerebraCoreTumblr.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.DTO
{
    public class Download
    {
        public bool Completed { get; set; }
        public string PostLinkID { get; set; }
        public string URLX { get; set; }

        public string FileName { get { return URLX.Substring(URLX.LastIndexOf(("/")) + 1); } set; }
        public string FileFullName { get { return DownloadPath + "\\" + URLX.Substring(URLX.LastIndexOf(("/")) + 1); } set; }
        public string DownloadPath { get; set; }
    }
}
