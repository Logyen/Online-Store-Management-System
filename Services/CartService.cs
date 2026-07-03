using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    internal class CartService
    {
        List<OrderItem> items;
        public CartService()
        {
            items = new List<OrderItem>();
        }
        public void AddToCart(OrderItem item)
        {
            items.Add(item);
        }
        public void RemoveFromCart(OrderItem item)
        {
            items.Remove(item);
        }
        public List<OrderItem> ViewCart()
        {
            return items;
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}
