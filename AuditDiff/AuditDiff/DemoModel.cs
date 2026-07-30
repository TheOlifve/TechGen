namespace AuditDiff;

public sealed class Money
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
}

public sealed class OrderLine
{
    public string Sku { get; set; } = "";
    public int Quantity { get; set; }

    public OrderLine(string sku, int quantity)
    {
        Sku = sku;
        Quantity = quantity;
    }
}

public sealed class Order
{
    public Guid Id { get; set; }
    [AuditName("Customer")]
    public string CustomerName { get; set; } = "";
    public string Status { get; set; } = "";
    public Money Total { get; set; } = new Money();
    public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
    public List<string> Tags { get; set; } = new List<string>();
    [AuditIgnore]
    public byte[]? RowVersion { get; set; }
    public Order()
    {
        Id = Guid.NewGuid();
        CustomerName = "Alex";
        Status = "Pending";
        Total.Amount = 1000;
        Lines.Add(new OrderLine("16H9UR6", 10));
        Lines.Add(new OrderLine("75K0AS1", 20));
        Lines.Add(new OrderLine("63I4LU8", 30));
        Tags.Add("Tag1");
        Tags.Add("Tag2");
        Tags.Add("Tag3");
        RowVersion = new byte[16];
        RowVersion = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
    }
}