using System.Collections.Generic;
using OnlineStore.Models;

namespace OnlineStore
{
    internal class StoreData
    {

        public List<Product> Products = new List<Product>
        {
            new Electronics { Id = 1,  Name = "Laptop",        Price = 15000, Stock = 10 },
            new Electronics { Id = 2,  Name = "Smartphone",    Price = 8000,  Stock = 25 },
            new Electronics { Id = 3,  Name = "Headphones",    Price = 1200,  Stock = 30 },
            new Electronics { Id = 4,  Name = "Tablet",        Price = 6500,  Stock = 15 },
            new Electronics { Id = 5,  Name = "Smart Watch",   Price = 3000,  Stock = 20 },
            new Electronics { Id = 16, Name = "Bluetooth Speaker", Price = 1800, Stock = 25 },
            new Electronics { Id = 17, Name = "Gaming Mouse", Price = 700, Stock = 40 },
            new Electronics { Id = 18, Name = "Mechanical Keyboard", Price = 1500, Stock = 30 },
            new Electronics { Id = 19, Name = "Monitor", Price = 4200, Stock = 18 },
            new Electronics { Id = 20, Name = "Power Bank", Price = 900, Stock = 50 },

            new Clothes { Id = 6,  Name = "Winter Jacket",  Price = 800,   Stock = 40 },
            new Clothes { Id = 7,  Name = "Jeans",          Price = 450,   Stock = 60 },
            new Clothes { Id = 8,  Name = "Dress",          Price = 600,   Stock = 35 },
            new Clothes { Id = 9,  Name = "T-Shirt",        Price = 200,   Stock = 100 },
            new Clothes { Id = 10, Name = "Sneakers",       Price = 950,   Stock = 50 },
            new Clothes { Id = 21, Name = "Hoodie", Price = 550, Stock = 45 },
            new Clothes { Id = 22, Name = "Shorts", Price = 300, Stock = 70 },
            new Clothes { Id = 23, Name = "Formal Shirt", Price = 650, Stock = 40 },
            new Clothes { Id = 24, Name = "Sports Pants", Price = 500, Stock = 35 },
            new Clothes { Id = 25, Name = "Cap", Price = 150, Stock = 90 },

            new Food { Id = 11, Name = "Olive Oil",      Price = 120,   Stock = 200 },
            new Food { Id = 12, Name = "Honey Jar",      Price = 85,    Stock = 150 },
            new Food { Id = 13, Name = "Protein Powder", Price = 350,   Stock = 80  },
            new Food { Id = 14, Name = "Mixed Nuts",     Price = 95,    Stock = 120 },
            new Food { Id = 15, Name = "Green Tea",      Price = 60,    Stock = 300 },
            new Food { Id = 26, Name = "Rice", Price = 40, Stock = 250 },
            new Food { Id = 27, Name = "Pasta", Price = 35, Stock = 180 },
            new Food { Id = 28, Name = "Coffee", Price = 180, Stock = 120 },
            new Food { Id = 29, Name = "Milk", Price = 30, Stock = 200 },
            new Food { Id = 30, Name = "Cheese", Price = 90, Stock = 150 },
        };

        public List<Customer> Customers = new List<Customer>
        {
            new Customer { Id = 1,  Name = "Sara Ali",       Email = "sara@mail.com" },
            new Customer { Id = 2,  Name = "Ahmed Mohamed",  Email = "ahmed@mail.com" },
            new Customer { Id = 3,  Name = "Mona Hassan",    Email = "mona@mail.com" },
            new Customer { Id = 4,  Name = "Omar Khaled",    Email = "omar@mail.com" },
            new Customer { Id = 5,  Name = "Dina Tarek",     Email = "dina@mail.com" },
            new Customer { Id = 6,  Name = "Youssef Adel",   Email = "youssef@mail.com" },
            new Customer { Id = 7,  Name = "Nour Ibrahim",   Email = "nour@mail.com" },
            new Customer { Id = 8,  Name = "Khaled Samir",   Email = "khaled@mail.com" },
            new Customer { Id = 9,  Name = "Salma Mostafa",  Email = "salma@mail.com" },
            new Customer { Id = 10, Name = "Ali Mahmoud",    Email = "ali@mail.com" }
        };

        public List<Order> Orders = new List<Order>();
    }
}
