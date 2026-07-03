using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore
{
    internal class OutOfStockException : Exception
    {
        public OutOfStockException() : base("Product is out of stock.") { }
    }
}
