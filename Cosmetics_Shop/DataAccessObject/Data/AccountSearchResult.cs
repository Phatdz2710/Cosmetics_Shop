using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{
    public class AccountSearchResult
    {
        public List<Account> ListAccounts { get; set; }
        public int TotalPages { get; set; }
        public int TotalAccounts { get; set; }
    }
}
