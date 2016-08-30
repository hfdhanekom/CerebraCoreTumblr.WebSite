using CerebraCoreTumblr.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Controller
{
    public class Download : IDownload
    {
        public IQueryable<DTO.Download> GetDownloads()
        {
            throw new NotImplementedException();
        }

        public void _Download(DTO.Download download)
        {
            Boolean Retry = true;
            int FailCount = 0;

            while (Retry)
            {
                try
                {
                    if (File.Exists(download.FileFullName))
                    {
                        Retry = false;
                        download.Completed = true;
                        return;
                    }

                    if (!Directory.Exists(download.DownloadPath))
                    {
                        Directory.CreateDirectory(download.DownloadPath);
                    }

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(download.URLX, download.FileFullName);
                        Retry = false;
                        download.Completed = true;
                    }
                }
                catch (Exception ex)
                {
                    FailCount++;
                    Retry = false;
                    if (File.Exists(download.FileFullName))
                    {
                        File.Delete(download.FileFullName);
                    }
                }
            }
        }


        public IQueryable<DTO.Download> CHKDownloads()
        {
            IList<DTO.Download> x = (new List<DTO.Download>()).ToList();


            return x.AsQueryable<DTO.Download>();
        }
    }
}
