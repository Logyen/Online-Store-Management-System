using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    internal class LinqServices
    {
        public void LinqMenu(List<Product> products, List<Customer> customers, List<Order> orders)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("╔══════════════════════════════════╗");
                Console.WriteLine("║         LINQ Reports             ║");
                Console.WriteLine("╠══════════════════════════════════╣");
                Console.WriteLine("║  1. Filter Products by Price     ║");
                Console.WriteLine("║  2. Sort Products by Name        ║");
                Console.WriteLine("║  3. Sort Products by Price       ║");
                Console.WriteLine("║  4. Search Product by Keyword    ║");
                Console.WriteLine("║  5. Top 5 Most Expensive         ║");
                Console.WriteLine("║  6. Customer Orders Summary      ║");
                Console.WriteLine("║  7. Today's Orders               ║");
                Console.WriteLine("║  0. Back to Main Menu            ║");
                Console.WriteLine("╚══════════════════════════════════╝");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.\n");
                    continue;
                }

                Console.WriteLine();

                switch (choice)
                {
                    case 1: FilterByPrice(products); break;
                    case 2: SortByName(products); break;
                    case 3: SortByPrice(products); break;
                    case 4: SearchByKeyword(products); break;
                    case 5: Top5Expensive(products); break;
                    case 6: CustomerOrdersSummary(customers, orders); break;
                    case 7: TodaysOrders(orders); break;
                    case 0: back = true; break;
                    default: Console.WriteLine("Please choose a number between 0 and 7."); break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

        // ── 1. Filter by Price ────────────────────────────────────────
        private void FilterByPrice(List<Product> products)
        {
            Console.Write("Show products with price above: ");
            if (!double.TryParse(Console.ReadLine(), out double minPrice))
            { Console.WriteLine("Invalid price."); return; }

            var result = products.Where(p => p.Price > minPrice).ToList();

            if (result.Count == 0)
            { Console.WriteLine($"No products above {minPrice} EGP."); return; }

            Console.WriteLine($"\nProducts above {minPrice} EGP ({result.Count} found):");
            Console.WriteLine($"{"ID",-4} {"Name",-20} {"Category",-12} {"Price",-10}");
            Console.WriteLine(new string('-', 46));
            foreach (var p in result)
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Category,-12} {p.Price,-10} EGP");
        }

        // ── 2. Sort by Name ───────────────────────────────────────────
        private void SortByName(List<Product> products)
        {
            var result = products.OrderBy(p => p.Name).ToList();

            Console.WriteLine($"\nAll Products (A → Z):");
            Console.WriteLine($"{"ID",-4} {"Name",-20} {"Category",-12} {"Price",-10}");
            Console.WriteLine(new string('-', 46));
            foreach (var p in result)
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Category,-12} {p.Price,-10} EGP");
        }

        // ── 3. Sort by Price ──────────────────────────────────────────
        private void SortByPrice(List<Product> products)
        {
            Console.WriteLine("Sort: 1-Highest first  2-Lowest first");
            Console.Write("Choice: ");
            int.TryParse(Console.ReadLine(), out int dir);

            var result = dir == 2
                ? products.OrderBy(p => p.Price).ToList()
                : products.OrderByDescending(p => p.Price).ToList();

            string label = dir == 2 ? "Lowest → Highest" : "Highest → Lowest";
            Console.WriteLine($"\nAll Products by Price ({label}):");
            Console.WriteLine($"{"ID",-4} {"Name",-20} {"Category",-12} {"Price",-10}");
            Console.WriteLine(new string('-', 46));
            foreach (var p in result)
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Category,-12} {p.Price,-10} EGP");
        }

        // ── 4. Search by Keyword ──────────────────────────────────────
        private void SearchByKeyword(List<Product> products)
        {
            Console.Write("Enter keyword (name or category): ");
            string keyword = Console.ReadLine();

            var result = products
                .Where(p => p.MatchesSearch(keyword))
                .ToList();

            if (result.Count == 0)
            { Console.WriteLine($"No products found for '{keyword}'."); return; }

            Console.WriteLine($"\nResults for '{keyword}' ({result.Count} found):");
            Console.WriteLine($"{"ID",-4} {"Name",-20} {"Category",-12} {"Price",-10}");
            Console.WriteLine(new string('-', 46));
            foreach (var p in result)
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Category,-12} {p.Price,-10} EGP");
        }

        // ── 5. Top 5 Most Expensive ───────────────────────────────────
        private void Top5Expensive(List<Product> products)
        {
            var result = products.OrderByDescending(p => p.Price).Take(5).ToList();

            Console.WriteLine("\nTop 5 Most Expensive Products:");
            Console.WriteLine($"{"Rank",-6} {"Name",-20} {"Category",-12} {"Price",-10}");
            Console.WriteLine(new string('-', 48));
            int rank = 1;
            foreach (var p in result)
                Console.WriteLine($"{rank++,-6} {p.Name,-20} {p.Category,-12} {p.Price,-10} EGP");
        }

        // ── 6. Customer Orders Summary ────────────────────────────────
        private void CustomerOrdersSummary(List<Customer> customers, List<Order> orders)
        {
            Console.Write("Enter Customer ID (or 0 for all customers): ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            { Console.WriteLine("Invalid ID."); return; }

            if (customerId == 0)
            {
                // Show all customers with their total spending
                var summary = customers.Join(
                    orders,
                    c => c.Id,
                    o => o.CustomerId,
                    (c, o) => new { CustomerName = c.Name, Total = o.CalculateTotal() })
                    .GroupBy(x => x.CustomerName)
                    .Select(g => new { Name = g.Key, TotalSpent = g.Sum(x => x.Total), OrderCount = g.Count() })
                    .OrderByDescending(x => x.TotalSpent)
                    .ToList();

                if (summary.Count == 0)
                { Console.WriteLine("No orders found."); return; }

                Console.WriteLine($"\n{"Customer",-20} {"Orders",-8} {"Total Spent",-15}");
                Console.WriteLine(new string('-', 43));
                foreach (var s in summary)
                    Console.WriteLine($"{s.Name,-20} {s.OrderCount,-8} {s.TotalSpent,-15:F2} EGP");
            }
            else
            {
                // Show specific customer's orders
                var customerOrders = orders.Where(o => o.CustomerId == customerId).ToList();
                var customer = customers.FirstOrDefault(c => c.Id == customerId);

                string name = customer?.Name ?? $"Customer #{customerId}";

                if (customerOrders.Count == 0)
                { Console.WriteLine($"No orders found for {name}."); return; }

                Console.WriteLine($"\nOrders for {name}:");
                Console.WriteLine($"{"Order ID",-10} {"Date",-14} {"Total",-12}");
                Console.WriteLine(new string('-', 36));
                foreach (var o in customerOrders)
                    Console.WriteLine($"{o.Id,-10} {o.Date,-14:dd/MM/yyyy} {o.CalculateTotal(),-12:F2} EGP");

                Console.WriteLine(new string('-', 36));
                Console.WriteLine($"Total Spent: {customerOrders.Sum(o => o.CalculateTotal()):F2} EGP");
            }
        }

        // ── 7. Today's Orders ─────────────────────────────────────────
        private void TodaysOrders(List<Order> orders)
        {
            var result = orders.Where(o => o.Date.Date == DateTime.Today).ToList();

            if (result.Count == 0)
            { Console.WriteLine("No orders placed today."); return; }

            Console.WriteLine($"\nToday's Orders ({result.Count} order(s)):");
            Console.WriteLine($"{"Order ID",-10} {"Customer ID",-14} {"Time",-10} {"Total",-12}");
            Console.WriteLine(new string('-', 46));
            foreach (var o in result)
                Console.WriteLine($"{o.Id,-10} {o.CustomerId,-14} {o.Date,-10:HH:mm} {o.CalculateTotal(),-12:F2} EGP");

            Console.WriteLine(new string('-', 46));
            Console.WriteLine($"Daily Revenue: {result.Sum(o => o.CalculateTotal()):F2} EGP");
        }
    }
}