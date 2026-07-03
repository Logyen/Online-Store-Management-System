using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    internal class Electronics : Product, IPrintable
    {
        public Electronics() { Category = ProductCategory.Electronics; }
        public override double CalculateDiscount()
        {
            return Price * 0.10;
        }
        public void PrintDetails()
        {
            Console.WriteLine($"Electronics:\n" +
                $"Id: {Id}\t Price: {Price}$\n" +
                $"Name:{Name}\t Stock:{Stock}");
        }
    }
}
