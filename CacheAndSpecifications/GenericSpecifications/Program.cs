namespace GenericSpecifications;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== E-commerce product filtering ===");
        Demo.RunProductDemo();
        
        Console.WriteLine("\n=== Loan approval rules ===");
        Demo.RunLoanDemo();
        
        Console.WriteLine("\n=== Job candidate screening ===");
        Demo.RunJobDemo();

        Console.WriteLine("\n=== Order fulfillment routing ===");
        Demo.RunOrderDemo();
    }
}