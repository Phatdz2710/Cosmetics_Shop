using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{
    /// <summary>
    /// Represents the result of a search for accounts.
    /// </summary>
    public class AccountSearchResult
    {
        /// <summary>
        /// List of accounts retrieved from the search.
        /// </summary>
        public List<Account> ListAccounts { get; set; }

        /// <summary>
        /// The total number of pages available for the account search.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The total number of accounts found in the search.
        /// </summary>
        public int TotalAccounts { get; set; }
    }
}
