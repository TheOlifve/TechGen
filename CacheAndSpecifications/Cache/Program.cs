namespace Cache;

class Program
{
    static void Main(string[] args)
    {
        Cache<int, string> tmp = new Cache<int, string>(2);
        tmp.Add(1, "1");
        String a;
        Console.WriteLine($"A - {tmp.TryGet(1, out a)}");
        Console.WriteLine("Waiting for 1 second....");
        Thread.Sleep(1000);
        Console.WriteLine("New element added");
        tmp.Add(2, "2");
        Console.WriteLine("Waiting for 1 second....");
        Thread.Sleep(1000);
        Console.WriteLine($"A - {tmp.TryGet(1, out a)}");
        Console.WriteLine($"B - {tmp.TryGet(2, out a)}");
        Console.WriteLine("Waiting for 1 second....");
        Thread.Sleep(1000);
        Console.WriteLine($"A - {tmp.TryGet(1, out a)}");
        Console.WriteLine($"B - {tmp.TryGet(2, out a)}");
    }
}