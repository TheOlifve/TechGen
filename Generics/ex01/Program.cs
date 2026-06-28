namespace ex01;

class GenericPair<T1, T2>
{
    public T1 First { get; }
    public T2 Second { get; }
    
    public GenericPair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }

    public GenericPair<T2, T1> SwapSides()
    {
        return new GenericPair<T2, T1>(Second, First);
    }

    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}

class Program
{
    static void Main(string[] args)
    {
        GenericPair<int, string> x = new GenericPair<int, string>(7, "seven");
        Console.WriteLine(x);
        Console.WriteLine(x.SwapSides());
    }
}