namespace CsvSerializer;

public static class CsvSerializer
{
    public static string WriteAll(object? obj)
    {
        Type type = obj?.GetType();
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            
        }
        return "str";
    }
}