using System;
using System.Collections.Generic;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    internal class OrderService
    {
        public event Action<Order>? OnOrderPlaced;

        private List<Order> orders;

        public OrderService()
        {
            orders = new List<Order>();
        }

        public void PlaceOrder(Order order)
        {
            // Validate stock for every item first, so a failure partway
            // through never leaves some products' stock already decremented.
            foreach (var item in order.OrderItems)
            {
                if (item.Quantity > item.Product.Stock)
                    throw new OutOfStockException();
            }

            foreach (var item in order.OrderItems)
            {
                item.Product.Stock -= item.Quantity;
            }

            orders.Add(order);
            OnOrderPlaced?.Invoke(order);
        }

        public List<Order> ViewOrders()
        {
            return orders;
        }

        public Order? GetOrderById(int id)
        {
            foreach (Order order in orders)
            {
                if (order.Id == id)
                    return order;
            }

            return null;
        }
    }
}