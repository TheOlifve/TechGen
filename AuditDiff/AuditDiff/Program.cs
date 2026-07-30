namespace AuditDiff;

class Program
{
    static void ChangeOrder(Order order)
    {
        order.CustomerName = "Alex L";
        order.Status = "Done";
        order.Total.Amount = 1200;
        order.Lines[1].Quantity = 22;
        order.Lines[2].Sku = "aaaaa";
        order.Tags[2] = "Tag4";
        order.RowVersion = [0, 2, 2, 3, 4, 5, 6, 7, 8, 100, 10, 11, 12, 13, 33, 15];
    }

    static void Main(string[] args)
    {
        Order before = new Order();
        Order after = new Order();

        // string a = "a";
        // string b = "b";
        
        ChangeOrder(after);
        // AuditDiff<string>.Diff(a, b);
        AuditDiff<Order>.Diff(before, after);
    }
}