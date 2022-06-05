
using ShoppingGameApp;

string[] arrSku = new string[] { "atv", "atv", "atv", "vga" };

PricingRules pricingRules = new PricingRules();
Checkout checkout = new Checkout(pricingRules.GetRules());

for (int i = 0; i < arrSku.Length; i++)
    checkout.Scan(arrSku[i].Trim());
decimal totalPrice = checkout.Total();

Console.WriteLine("Total Price: " + totalPrice.ToString("C"));
Console.ReadLine();