namespace CsvSerializer;

public sealed class ProductRow
{
    [CsvColumn("SKU", 1)]
    public string Sku { get; set; } = "";
    [CsvColumn("Product Name", 2)]
    public string Name { get; set; } = "";
    [CsvColumn("Unit Price", 3)]
    public decimal Price { get; set; }
    [CsvColumn("In Stock", 4)]
    public bool InStock { get; set; }
    [CsvIgnore]
    public string? WarehouseCode { get; set; }
}