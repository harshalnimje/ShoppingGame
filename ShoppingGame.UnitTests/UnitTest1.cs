using Xunit;
using ShoppingGameApp;

namespace ShoppingGame.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string[] arrSku = new string[] { "atv", "atv", "atv", "vga" };

            PricingRules pricingRules = new PricingRules();
            Checkout checkout = new Checkout(pricingRules.GetRules());

            for (int i = 0; i < arrSku.Length; i++)
                checkout.Scan(arrSku[i].Trim());
            decimal totalPrice = checkout.Total();

            string expectedResult = totalPrice.ToString("C");
            string actualResult = "$249.00";
            Assert.Equal(expectedResult,actualResult);
        }

        [Fact]
        public void Test2()
        {
            string[] arrSku = new string[] { "atv", "ipd", "ipd", "atv", "ipd", "ipd", "ipd" };

            PricingRules pricingRules = new PricingRules();
            Checkout checkout = new Checkout(pricingRules.GetRules());

            for (int i = 0; i < arrSku.Length; i++)
                checkout.Scan(arrSku[i].Trim());
            decimal totalPrice = checkout.Total();

            string expectedResult = totalPrice.ToString("C");
            string actualResult = "$2,718.95";
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Test3()
        {
            string[] arrSku = new string[] { "mbp", "vga", "ipd" };

            PricingRules pricingRules = new PricingRules();
            Checkout checkout = new Checkout(pricingRules.GetRules());

            for (int i = 0; i < arrSku.Length; i++)
                checkout.Scan(arrSku[i].Trim());
            decimal totalPrice = checkout.Total();

            string expectedResult = totalPrice.ToString("C");
            string actualResult = "$1,949.98";
            Assert.Equal(expectedResult, actualResult);
        }
    }
}