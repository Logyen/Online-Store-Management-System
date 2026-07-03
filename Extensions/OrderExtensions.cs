using System.Text;
using OnlineStore.Models;

namespace OnlineStore
{
    internal static class OrderExtensions
    {
        public static string ToFormattedString(this Order order)
        {
            StringBuilder receipt = new StringBuilder();

            receipt.AppendLine("========================================");
            receipt.AppendLine("              ONLINE STORE");
            receipt.AppendLine("========================================");
            receipt.AppendLine();

            receipt.AppendLine($"Order ID    : {order.Id}");
            receipt.AppendLine($"Customer ID : {order.CustomerId}");
            receipt.AppendLine($"Date        : {order.Date}");
            receipt.AppendLine();

            receipt.AppendLine("----------------------------------------");
            receipt.AppendLine("Items:");
            receipt.AppendLine();

            int count = 0;

            foreach (var item in order.OrderItems)
            {
                count++;

                double discount = item.Product.CalculateDiscount();
                double itemTotal = (item.Product.Price - discount) * item.Quantity;

                receipt.AppendLine($"{count}. {item.Product.Name}");
                receipt.AppendLine($"\tPrice      : {item.Product.Price} EGP");
                receipt.AppendLine($"\tQuantity   : {item.Quantity}");
                receipt.AppendLine($"\tDiscount   : {discount} EGP");
                receipt.AppendLine($"\tItem Total : {itemTotal} EGP");
                receipt.AppendLine();
            }

            receipt.AppendLine("----------------------------------------");
            receipt.AppendLine($"Grand Total : {order.CalculateTotal()} EGP");
            receipt.AppendLine();
            receipt.AppendLine("========================================");
            receipt.AppendLine("      Thank you for your purchase!");
            receipt.AppendLine("========================================");

            return receipt.ToString();
        }
    }
}