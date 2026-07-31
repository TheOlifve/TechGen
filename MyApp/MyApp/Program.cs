using ACA.PriceEngine;
using WraperPriceEngine;

namespace MyApp;

class Program
{
    static void Fill(PriceInput input)
    {
        input.CouponAmount = 1;
        BasketLine line = new BasketLine();
        BasketLine line2 = new BasketLine();
        BasketLine line3 = new BasketLine();
        
        line.Quantity = 10;
        line2.Quantity = 20;
        line3.Quantity = 30;

        line.Sku = "AULD34KJ";
        line2.Sku = "AU34KJ";
        line3.Sku = "AULKJ";

        line.UnitPrice = 16;
        line2.UnitPrice = 12;
        line3.UnitPrice = 13;
        
        input.Lines.Add(line);
        input.Lines.Add(line2);
        input.Lines.Add(line3);
        
        input.LoyaltyTier = 29;
        input.VatRate = 10;
    }
    
    static void Main(string[] args)
    {
        WrapPriceEngine wrapedEngine = new WrapPriceEngine();
        PriceEngine priceEngine = new PriceEngine();
        PriceInput input = new PriceInput();
        Fill(input);
        
        Console.WriteLine($"Price Engine - {priceEngine.CalculatePayable(input)}");
        Console.WriteLine($"Wraped Engine - {wrapedEngine.CalculatePayable(input)}");
    }
}