using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.DataService
{
    public interface IDao
    {
        void GetAllProduct();
        void GetProductByKeyword(string keyword);
    }
}
