namespace CsvSerializer;

class Program
{
    static void Main(string[] args)
    {
        List<ProductRow> list2 = new List<ProductRow>();
        ProductRow product1 = new ProductRow();
        product1.Sku = "TEA-1";
        product1.Name = "Armenian Tea";
        product1.Price = 4.50m;
        product1.InStock = true;
        list2.Add(product1);
        
        ProductRow product2 = new ProductRow();
        product2.Sku = "COF-2";
        product2.Name = "Coffee Premium";
        product2.Price = 9.99m;
        product2.InStock = false;
        list2.Add(product2);
        
        string csv = CsvSerializer.WriteAll(list2);
        Console.WriteLine(csv);
        List<ProductRow> rows = CsvSerializer.ReadAll<ProductRow>(csv);
        foreach (ProductRow row in rows)
        {
            Console.WriteLine($"{row.Sku},  {row.Name}, {row.Price}, {row.InStock}");
        }
    }
}