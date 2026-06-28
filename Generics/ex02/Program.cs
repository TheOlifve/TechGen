using System.Text;

namespace ex02;

class Program
{
    static void PrintList<T>(List<T> list)
    {
        StringBuilder retString = new StringBuilder();

        retString.Append("[");
        foreach (var item in list)
            retString.Append($" {item.ToString()} ");
        retString.Append("]"); 
        Console.WriteLine(retString);
    }
    
    static void Main(string[] args)
    {
        StringBuilder retString = new StringBuilder();
        List<int> Items = new List<int>();
        Items.Add(1);
        Items.Add(2);
        Items.Add(3);
        Items.Add(4);
        Items.Add(5);
        Filter<int> filter = new Filter<int>(x => x % 2 == 0);
        filter.FilterList(Items);
        PrintList(filter.FilteredItems);

        Project<int, string> project = new Project<int, string>(n => $"N{n}");
        var projectedList = project.ProjectList(filter.FilteredItems);
        PrintList(projectedList);
    }
}