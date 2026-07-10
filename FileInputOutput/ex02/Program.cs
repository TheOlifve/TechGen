namespace ex02;

class Program
{
    static void Main(string[] args)
    {
        var absolutePath = Path.GetFullPath(System.Environment.CurrentDirectory);
        
        InboxScanner.Scan(absolutePath);
    }
}