using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    abstract class Product : BaseEntity, ISearchable, IDiscountable
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public ProductCategory Category { get; set; }
        public abstract double CalculateDiscount();
        public bool MatchesSearch(string keyword)
        {
            return Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                 || Category.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
