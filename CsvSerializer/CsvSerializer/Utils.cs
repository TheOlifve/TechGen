using System.Globalization;

namespace CsvSerializer;

public class CsvPropertyInfo
{
    public string Name { get; set; }
    public string? Value { get; set; }
    public int Priority { get; set; }

    public CsvPropertyInfo(string name, object? value,  int priority = int.MaxValue)
    {
        CultureInfo.CurrentCulture = new CultureInfo("Invariant");
        Name = name;
        Value = value == null ? "(missing)" : value.ToString();
        // if (Value.Contains(",") || Value.Contains("\n") || Value.Contains(""))
            
        Priority = priority;
    }
}