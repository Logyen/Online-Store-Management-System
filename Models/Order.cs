using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    internal class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (OrderItem item in OrderItems)
            {
                double priceAfterDiscount =
                    (item.Product.Price - item.Product.CalculateDiscount()) * item.Quantity;
                total += priceAfterDiscount;
            }
            return total;
        }
    }
}
