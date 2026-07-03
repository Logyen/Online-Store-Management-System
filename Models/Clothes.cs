using System;

namespace OnlineStore.Models
{
    internal class Clothes : Product, IPrintable
    {
        public Clothes() { Category = ProductCategory.Clothes; }

        public override double CalculateDiscount()
        {
            return Price > 500 ? Price * 0.20 : 0;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Clothes:\nId: {Id}\t Price: {Price}$\nName:{Name}\t Stock:{Stock}");
        }
    }
}
