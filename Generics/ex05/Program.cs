namespace ex05;

using System;

class Program
{
    static void Main()
    {
        int count = 0;

        Result<int> result = Executor<int>.Execute(
            () =>
            {
                count++;

                Console.WriteLine($"Attempt {count}");

                if (count == 1)
                    throw new InvalidOperationException();

                throw new ArgumentException();
                return 1;
            },
            5,
            ex => ex is InvalidOperationException
        );
        
        Console.WriteLine();
        Console.WriteLine($"Success : {result.Success}");
        Console.WriteLine($"Attempts: {result.Attempts}");
        if (result.Success)
            Console.WriteLine($"Value   : {result.Value}");
        else
            Console.WriteLine($"Error   : {result.Error}");
    }
}