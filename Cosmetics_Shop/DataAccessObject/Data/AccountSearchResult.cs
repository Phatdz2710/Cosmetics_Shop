using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{
    /// <summary>
    /// Account Search Result
    /// </summary>
    public class AccountSearchResult
    {
        // List of accounts
        public List<Account> ListAccounts { get; set; }

        // Total pages of accounts
        public int TotalPages { get; set; }

        // Total accounts
        public int TotalAccounts { get; set; }
    }
}
