using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Interface
{
    public interface IDownload
    {
        IQueryable<DTO.Download> GetDownloads();
        IQueryable<DTO.Download> CHKDownloads();
        void _Download(DTO.Download download);
    }
}
