using CerebraCoreTumblr.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Controller
{
    public class TumblrAccount : ITumblrAccount
    {
        public IQueryable<DTO.TumblrAccount> GetTumblrAccounts()
        {
            throw new NotImplementedException();
        }

        public DTO.TumblrAccount GetTumblrAccount(string Email)
        {
            throw new NotImplementedException();
        }

        public void AddTumblrAccount(DTO.TumblrAccount NewAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteTumblrAccount(string Email)
        {
            throw new NotImplementedException();
        }
    }


}
