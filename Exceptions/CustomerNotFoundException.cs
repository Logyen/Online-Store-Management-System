using System;

namespace OnlineStore
{
    internal class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base("Customer not found.") { }
    }
}
