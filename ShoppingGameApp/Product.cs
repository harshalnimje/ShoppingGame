using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGameApp
{
    public class Product
    {
        private string sku = string.Empty;
        private string name = string.Empty;
        private decimal price = 0;
        private static List<Product> products = null;

        public string SKU
        {
            get { return sku; }
            set { sku = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private static List<Product> GetProducts()
        {
            if (products == null)
            {
                products = new List<Product>
                {
                    new Product { SKU = "ipd", Name="Super iPad", Price = 549.99M},
                    new Product { SKU = "mbp", Name="MacBook Pro", Price = 1399.99M},
                    new Product { SKU = "atv", Name="Apple TV", Price = 109.50M},
                    new Product { SKU = "vga", Name="VGA adapter", Price = 30.00M}
                };
            }
            return products;
        }

        public static Product GetProduct(string sku)
        {
            if (products == null)
                GetProducts();

            return products.Where(p => p.SKU == sku).FirstOrDefault();
        }
    }
}
