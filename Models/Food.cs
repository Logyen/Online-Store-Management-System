using System;

namespace OnlineStore.Models
{
    internal class Food : Product, IPrintable
    {
        public Food() { Category = ProductCategory.Food; }

        public override double CalculateDiscount() => 0;

        public void PrintDetails()
        {
            Console.WriteLine($"Food:\nId: {Id}\t Price: {Price}$\nName:{Name}\t Stock:{Stock}");
        }
    }
}
