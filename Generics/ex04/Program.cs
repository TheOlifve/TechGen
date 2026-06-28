using System.Text;

namespace ex04;

class Program
{
    static void PrintArray<T>(T[] list)
    {
        StringBuilder retString = new();

        retString.Append("[");
        foreach (var item in list)
            retString.Append($" {item.ToString()} ");
        retString.Append("]"); 
        Console.WriteLine(retString);
    }
    
    static void Main(string[] args)
    {
        TopBuffer<int> buff = new TopBuffer<int>(3);
        int[] arr = {5,1,9,3,7,2 };
        buff.FindTopElements(arr);
        PrintArray(buff.Buffer);
    }
}