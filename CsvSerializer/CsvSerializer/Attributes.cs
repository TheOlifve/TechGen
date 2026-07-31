namespace CsvSerializer;

public class CsvColumnAttribute: Attribute
{
    public string Name { get; set; }
    public int Priority { get; set; }
    
    public CsvColumnAttribute(string name = "", int priority = int.MaxValue)
    {
        Name = name;
        Priority = priority;
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class CsvIgnoreAttribute: Attribute
{
    public CsvIgnoreAttribute() { }
}