using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore
{
    internal class Program
    {
        static ProductService  productService  = new ProductService();
        static OrderService    orderService    = new OrderService();
        static CustomerService customerService = new CustomerService();
        static CartService     cartService     = new CartService();
        static LinqServices    linqServices    = new LinqServices();
        static StoreData       storeData       = new StoreData();

        static void Main(string[] args)
        {
            // Ensure box-drawing characters and emoji render correctly on Windows consoles
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Load seed data
            foreach (var p in storeData.Products)  productService.AddProduct(p);
            foreach (var c in storeData.Customers) customerService.AddCustomer(c);

            // Event: print confirmation whenever an order is placed
            orderService.OnOrderPlaced += order =>
                Console.WriteLine($"\n✅ Order #{order.Id} placed successfully! Total: {order.CalculateTotal()} EGP\n");

            Run();
        }

        // ── Main loop ────────────────────────────────────────────────
        static void Run()
        {
            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": ShowAllProducts();   break;
                    case "2": AddProduct();        break;
                    case "3": SearchProduct();     break;
                    case "4": AddToCart();         break;
                    case "5": ViewCart();          break;
                    case "6": PlaceOrder();        break;
                    case "7": ViewOrders();        break;
                    case "8": ViewCustomers();     break;
                    case "9": LinqReports();       break;
                    case "10": RemoveProduct();    break;
                    case "11": RemoveFromCart();   break;
                    case "12": ViewOrderById();    break;
                    case "13": PrintProductDetails(); break;
                    case "0": exit = true;         break;
                    default:  Console.WriteLine("Invalid choice. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("Goodbye!");
        }

        static void ShowMenu()
        {
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       ONLINE STORE           ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. View All Products        ║");
            Console.WriteLine("║  2. Add New Product          ║");
            Console.WriteLine("║  3. Search Product           ║");
            Console.WriteLine("║  4. Add Product to Cart      ║");
            Console.WriteLine("║  5. View Cart                ║");
            Console.WriteLine("║  6. Place Order              ║");
            Console.WriteLine("║  7. View All Orders          ║");
            Console.WriteLine("║  8. View Customers           ║");
            Console.WriteLine("║  9. LINQ Reports             ║");
            Console.WriteLine("║ 10. Remove Product           ║");
            Console.WriteLine("║ 11. Remove Item From Cart    ║");
            Console.WriteLine("║ 12. View Order By ID         ║");
            Console.WriteLine("║ 13. Print Product Details    ║");
            Console.WriteLine("║  0. Exit                     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("Choice: ");
        }

        // ── 1. Show All Products ──────────────────────────────────────
        static void ShowAllProducts()
        {
            var products = productService.ViewAllProducts();
            if (products.Count == 0) { Console.WriteLine("No products available."); return; }

            Console.WriteLine($"{"ID",-4} {"Name",-20} {"Category",-12} {"Price",-10} {"Stock",-6} {"Discounted Price",-16}");
            Console.WriteLine(new string('-', 68));
            foreach (var p in products)
            {
                double finalPrice = p.Price - p.CalculateDiscount();
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Category,-12} {p.Price,-10} {p.Stock,-6} {finalPrice,-16}");
            }
        }

        // ── 2. Add New Product ────────────────────────────────────────
        static void AddProduct()
        {
            Console.WriteLine("Category: 1-Electronics  2-Clothes  3-Food");
            Console.Write("Choice: ");
            if (!int.TryParse(Console.ReadLine(), out int cat) || cat < 1 || cat > 3)
            { Console.WriteLine("Invalid category."); return; }

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            { Console.WriteLine("Invalid price."); return; }

            Console.Write("Stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            { Console.WriteLine("Invalid stock."); return; }

            var existingProducts = productService.ViewAllProducts();
            int newId = existingProducts.Count == 0
                ? 1 : existingProducts.Max(p => p.Id) + 1;

            Product product = cat switch
            {
                1 => new Electronics { Id = newId, Name = name, Price = price, Stock = stock },
                2 => new Clothes     { Id = newId, Name = name, Price = price, Stock = stock },
                _ => new Food        { Id = newId, Name = name, Price = price, Stock = stock },
            };

            productService.AddProduct(product);
            Console.WriteLine($"Product '{name}' added with ID {newId}.");
        }

        // ── 3. Search Product ─────────────────────────────────────────
        static void SearchProduct()
        {
            Console.Write("Enter keyword or category: ");
            string keyword = Console.ReadLine();

            var results = productService.ViewAllProducts()
                .Where(p => p.MatchesSearch(keyword))
                .ToList();

            if (results.Count == 0) { Console.WriteLine("No products found."); return; }

            Console.WriteLine($"\nFound {results.Count} product(s):");
            foreach (var p in results)
                Console.WriteLine($"  [{p.Id}] {p.Name} — {p.Price} EGP  (Stock: {p.Stock})");
        }

        // ── 4. Add to Cart ────────────────────────────────────────────
        static void AddToCart()
        {
            ShowAllProducts();
            Console.Write("\nProduct ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("Invalid ID."); return; }

            try
            {
                Product product = productService.GetProductById(id);

                Console.Write("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                { Console.WriteLine("Invalid quantity."); return; }

                cartService.AddToCart(new OrderItem { Product = product, Quantity = qty });
                Console.WriteLine($"'{product.Name}' x{qty} added to cart.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ── 5. View Cart ──────────────────────────────────────────────
        static void ViewCart()
        {
            var items = cartService.ViewCart();
            if (items.Count == 0) { Console.WriteLine("Cart is empty."); return; }

            Console.WriteLine($"\n{"Product",-20} {"Price",-10} {"Qty",-6} {"Subtotal",-10}");
            Console.WriteLine(new string('-', 46));
            double total = 0;
            foreach (var item in items)
            {
                double sub = (item.Product.Price - item.Product.CalculateDiscount()) * item.Quantity;
                total += sub;
                Console.WriteLine($"{item.Product.Name,-20} {item.Product.Price,-10} {item.Quantity,-6} {sub,-10}");
            }
            Console.WriteLine(new string('-', 46));
            Console.WriteLine($"{"Total",-36} {total} EGP");
        }

        // ── 6. Place Order ────────────────────────────────────────────
        static void PlaceOrder()
        {
            var items = cartService.ViewCart();
            if (items.Count == 0) { Console.WriteLine("Cart is empty. Add products first."); return; }

            Console.Write("Your Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            { Console.WriteLine("Invalid ID."); return; }

            try
            {
                if (customerService.SearchCustomer(customerId) == null)
                    throw new CustomerNotFoundException();

                var existingOrders = orderService.ViewOrders();
                int newOrderId = existingOrders.Count == 0
                    ? 1 : existingOrders.Max(o => o.Id) + 1;

                Order order = new Order
                {
                    Id         = newOrderId,
                    CustomerId = customerId,
                    Date       = DateTime.Now
                };

                foreach (var item in items)
                    order.OrderItems.Add(item);

                orderService.PlaceOrder(order);
                Console.WriteLine(order.ToFormattedString());
                cartService.ClearCart();
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
            }
            catch (OutOfStockException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
            }
        }

        // ── 7. View All Orders ────────────────────────────────────────
        static void ViewOrders()
        {
            var orders = orderService.ViewOrders();
            if (orders.Count == 0) { Console.WriteLine("No orders yet."); return; }

            foreach (var o in orders)
            {
                Console.WriteLine($"Order #{o.Id} | Customer ID: {o.CustomerId} | Date: {o.Date:dd/MM/yyyy} | Total: {o.CalculateTotal()} EGP");
            }
        }

        // ── 8. View Customers ─────────────────────────────────────────
        static void ViewCustomers()
        {
            var customers = customerService.GetAllCustomers();
            Console.WriteLine($"\n{"ID",-4} {"Name",-20} {"Email",-25}");
            Console.WriteLine(new string('-', 50));
            foreach (var c in customers)
                Console.WriteLine($"{c.Id,-4} {c.Name,-20} {c.Email,-25}");
        }

        // ── 10. Remove Product ────────────────────────────────────────
        static void RemoveProduct()
        {
            ShowAllProducts();
            Console.Write("\nProduct ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("Invalid ID."); return; }

            try
            {
                Product product = productService.GetProductById(id);
                productService.RemoveProduct(product);
                Console.WriteLine($"'{product.Name}' removed.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ── 11. Remove Item From Cart ─────────────────────────────────
        static void RemoveFromCart()
        {
            var items = cartService.ViewCart();
            if (items.Count == 0) { Console.WriteLine("Cart is empty."); return; }

            for (int i = 0; i < items.Count; i++)
                Console.WriteLine($"  {i + 1}. {items[i].Product.Name} x{items[i].Quantity}");

            Console.Write("\nLine number to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int line) || line < 1 || line > items.Count)
            { Console.WriteLine("Invalid line number."); return; }

            var item = items[line - 1];
            cartService.RemoveFromCart(item);
            Console.WriteLine($"'{item.Product.Name}' removed from cart.");
        }

        // ── 12. View Order By ID ──────────────────────────────────────
        static void ViewOrderById()
        {
            Console.Write("Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("Invalid ID."); return; }

            var order = orderService.GetOrderById(id);
            if (order == null) { Console.WriteLine("Order not found."); return; }

            Console.WriteLine(order.ToFormattedString());
        }

        // ── 13. Print Product Details ─────────────────────────────────
        static void PrintProductDetails()
        {
            ShowAllProducts();
            Console.Write("\nProduct ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("Invalid ID."); return; }

            try
            {
                Product product = productService.GetProductById(id);
                if (product is IPrintable printable)
                    printable.PrintDetails();
                else
                    Console.WriteLine("This product does not support detailed printing.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ── 9. LINQ Reports ───────────────────────────────────────────
        static void LinqReports()
        {
            linqServices.LinqMenu(
                productService.ViewAllProducts(),
                customerService.GetAllCustomers(),
                orderService.ViewOrders());
        }
    }
}
