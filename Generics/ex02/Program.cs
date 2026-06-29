using System.Text;

namespace ex02;

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
        StringBuilder retString = new StringBuilder();
        int[] Items = { 1, 2, 3, 4, 5 };
        
        Filter<int> filter = new Filter<int>(x => x % 2 == 0);
        filter.FilterList(Items);
        PrintArray(filter.FilteredItems);

        Project<int, string> project = new Project<int, string>(n => $"N{n}");
        var projectedList = project.ProjectList(filter.FilteredItems);
        PrintArray(projectedList);
    }
}