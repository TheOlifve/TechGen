using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace CsvSerializer;


public static class CsvSerializer
{
    private static Dictionary<string, PropertyInfo[]> _cache = new Dictionary<string, PropertyInfo[]>();

    private static PropertyInfo[] Cache(Type type)
    {
        if (_cache.ContainsKey(type.FullName))
            return _cache[type.FullName];
     
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        _cache.Add(type.FullName, properties);
        
        return _cache[type.FullName];
    }

    private static Type? Validate(object? obj)
    {
        Type? type = obj.GetType();
        
        if (!type.FullName.Contains("Collections"))
            throw new ArgumentException($"Type {type.FullName} must contain Collections");
        
        return type;
    }

    private static Type? GetType(IEnumerator enumerator)
    {
        if (!enumerator.MoveNext())
            return null;
        return enumerator.Current?.GetType();
    }
    
    public static string WriteAll(object? obj)
    {
        Type? type = Validate(obj);
        
        IEnumerable enumerable = (IEnumerable)obj;
        
        Type? elementType = GetType(enumerable.GetEnumerator());
        
        if (elementType == null)
            throw new NullReferenceException($"Type {type.FullName} cannot be empty");
        
        PropertyInfo[] properties = Cache(elementType);

        List<CsvPropertyInfo> list = new List<CsvPropertyInfo>();
        
        foreach (var element in enumerable)
        {
            list.Clear();
            foreach (var property in properties)
            {
                CsvIgnoreAttribute? ignoreAttribute = property.GetCustomAttribute<CsvIgnoreAttribute>();
                CsvColumnAttribute? columnAttribute = property.GetCustomAttribute<CsvColumnAttribute>();
                
                if (ignoreAttribute != null)
                    continue;
                
                if (columnAttribute != null)
                    list.Add(new CsvPropertyInfo(columnAttribute.Name, property.GetValue(element), columnAttribute.Priority));
                else
                    list.Add(new CsvPropertyInfo(property.Name, property.GetValue(element)));
            }
            list.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            CsvLogger.Log(list);
        }
        CsvLogger.CreateHeader(list);
        return CsvLogger.GetLog();
    }

    public static List<T> ReadAll<T>(string csv)
    {
        List<T> ret = new List<T>();
        Type    type = typeof(T);
        PropertyInfo[] properties = Cache(type);
        string[] rows = csv.Split('\n');
        
        if (rows.Length == 0)
            return ret;
        
        string[] header = rows[0].Split(',');
        header.TrimWhitespaces();

        for (int i = 1; i < rows.Length - 1; i++)
        {
            Dictionary<string, object?> dict = new Dictionary<string, object?>();
            string[] row = rows[i].Split(',');
            row.TrimWhitespaces();
            
            for (int j = 0; j < row.Length; j++)
            {
                dict.Add(header[j], row[j]);
            }
            
            object? instance = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                if (property.CanWrite == false)
                    continue;   
                CsvColumnAttribute? columnAttribute = property.GetCustomAttribute<CsvColumnAttribute>();

                string? propertyName = columnAttribute == null ? property.Name : columnAttribute.Name;
                if (dict.ContainsKey(propertyName))
                    property.SetValue(instance, Convert.ChangeType(dict[propertyName], property.PropertyType));
            }
            ret.Add((T)instance);
        }

        return ret;
    }
}