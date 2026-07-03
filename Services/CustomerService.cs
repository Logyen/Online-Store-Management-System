using System.Collections.Generic;
using System.Linq;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    internal class CustomerService
    {
        private List<Customer> customers;

        public CustomerService()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public Customer SearchCustomer(int id)
        {
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }
    }
}
