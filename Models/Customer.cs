using System.Collections.Generic;

namespace OnlineStore.Models
{
    internal class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
