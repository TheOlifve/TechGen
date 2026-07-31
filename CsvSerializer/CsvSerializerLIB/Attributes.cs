namespace CsvSerializer;

public class CsvColumnAttribute: Attribute
{
    public string Name { get; set; }
    public int Priority { get; set; }
    
    public CsvColumnAttribute() { Name = string.Empty; Priority = -1; }
    
    public CsvColumnAttribute(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }
}

public class CsvIgnoreAttribute: Attribute
{
    public CsvIgnoreAttribute() { }
}