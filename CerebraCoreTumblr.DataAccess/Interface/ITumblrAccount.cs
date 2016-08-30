using CerebraCoreTumblr.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Interface
{
    public interface ITumblrAccount
    {
        IQueryable<TumblrAccount> GetTumblrAccounts();

        TumblrAccount GetTumblrAccount(String Email);
        void AddTumblrAccount(TumblrAccount NewAccount);
        void DeleteTumblrAccount(String Email);
    }
}
