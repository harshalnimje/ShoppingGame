using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGameApp
{
    public class Checkout
    {
        List<PricingRule> _rules;
        List<AddToCart> _items = new List<AddToCart>();

        public Checkout(List<PricingRule> rules)
        {
            _rules = rules;
        }

        public void Scan(string item)
        {
            var product = Product.GetProduct(item);
            if (product != null)
                _items.Add(new AddToCart { SKU = product.SKU, Price = product.Price });
        }

        public decimal Total()
        {
            decimal total = 0;

            var list = _items.GroupBy(x => x.SKU)
                        .Select(group => new
                        {
                            sku = group.Key,
                            items = group.ToList()
                        })
                        .ToList();

            foreach (var item in list)
            {
                var rule = _rules.FirstOrDefault(x => x.sku == item.sku);
                if (rule != null)
                {
                    switch (rule.type)
                    {
                        case PricingRuleType.ThreeForTwoDeal:
                            {
                                var product = Product.GetProduct(item.sku);
                                if (Convert.ToInt32(rule.attributes["Qty"]) == item.items.Count)
                                {
                                    ThreeForTwoDeal threeForTwoDeal = new ThreeForTwoDeal(product, item.items.Count);
                                    total += threeForTwoDeal.CalculatePrice();
                                }
                                else
                                {
                                    total += product.Price * item.items.Count;
                                }
                            }
                            break;
                        case PricingRuleType.BulkDiscount:
                            {
                                var product = Product.GetProduct(item.sku);
                                if (item.items.Count > Convert.ToInt32(rule.attributes["QtyGreaterThan"]))
                                {
                                    decimal price = 0.00M;
                                    decimal.TryParse(rule.attributes["Price"], out price);

                                    BulkDiscount bulkDiscount = new BulkDiscount(price, item.items.Count);
                                    total += bulkDiscount.CalculatePrice();
                                }
                                else
                                {
                                    total += product.Price * item.items.Count;
                                }
                            }
                            break;
                        case PricingRuleType.FreeProduct:
                            {
                                var product = Product.GetProduct(item.sku);
                                if (rule.attributes["PrimaryProduct"].ToString() == product.SKU && list.Any(x => x.sku == rule.attributes["FreeProduct"].ToString()))
                                {

                                    var freeProduct = Product.GetProduct(rule.attributes["FreeProduct"].ToString());
                                    total += (product.Price * item.items.Count) - (freeProduct.Price * list.Where(x => x.sku == rule.attributes["FreeProduct"].ToString()).Count());
                                }
                            }
                            break;
                        default:
                            {
                                var product = Product.GetProduct(item.sku);
                                total += product.Price * item.items.Count;
                            }
                            break;
                    }
                }
                else
                {
                    var product = Product.GetProduct(item.sku);
                    total += product.Price * item.items.Count;
                }
            }

            return total;
        }

        private class AddToCart
        {
            public string SKU { get; set; }
            public decimal Price { get; set; }
        }
    }
}
