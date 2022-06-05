using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGameApp
{
    public enum PricingRuleType
    {
        ThreeForTwoDeal = 1,
        BulkDiscount = 2,
        FreeProduct = 3
    }

    public class PricingRules
    {
        List<PricingRule> rules = new List<PricingRule>();
        public List<PricingRule> GetRules()
        {
            Dictionary<string, string> attrs;

            //Rule 1
            PricingRule rule = new PricingRule();
            rule.sku = "atv";
            rule.type = PricingRuleType.ThreeForTwoDeal;
            attrs = new Dictionary<string, string>();
            attrs.Add("Qty", "3");
            rule.attributes = attrs;
            rules.Add(rule);

            //Rule 2
            rule = new PricingRule();
            rule.sku = "ipd";
            rule.type = PricingRuleType.BulkDiscount;
            attrs = new Dictionary<string, string>();
            attrs.Add("QtyGreaterThan", "4");
            attrs.Add("Price", "499.99");
            rule.attributes = attrs;
            rules.Add(rule);

            //Rule 3
            rule = new PricingRule();
            rule.sku = "mbp";
            rule.type = PricingRuleType.FreeProduct;
            attrs = new Dictionary<string, string>();
            attrs.Add("PrimaryProduct", "mbp");
            attrs.Add("FreeProduct", "vga");
            rule.attributes = attrs;
            rules.Add(rule);

            return rules;
        }
    }


    public class ThreeForTwoDeal
    {
        Product _product;
        int _qty;
        public ThreeForTwoDeal(Product product, int qty)
        {
            _product = product;
            _qty = qty;
        }

        public decimal CalculatePrice()
        {
            return (_product.Price * _qty) - _product.Price;
        }
    }

    public class BulkDiscount
    {
        int _qty;
        decimal _price;
        public BulkDiscount(decimal price, int qty)
        {
            _qty = qty;
            _price = price;
        }

        public decimal CalculatePrice()
        {
            return _price * _qty;
        }
    }

}
