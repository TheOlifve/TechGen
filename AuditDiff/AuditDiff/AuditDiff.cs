using System.Collections;
using System.Reflection;

namespace AuditDiff;

public class AuditDiff
{
    private bool IsScalar(Type type)
    {
        return (type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                || type == typeof(DateTimeOffset)
                || type == typeof(TimeSpan)
                || type == typeof(Guid));
    }

    private void LogDiff(object? before, object? after, string fullPath)
    {
        object? beforeValue = before == null ? "(missing)" : before;
        object? afterValue = after == null ? "(missing)" : after;
        
        Console.WriteLine($"{fullPath} | {beforeValue} | {afterValue}");
    }
    
    private void FindDiff(object? before, object? after, string? path = null, string? flag = null)
    {
        Type type = before.GetType();
        string fullPath = path == null ? type.Name : flag == "Collection" ? $"{path}" : $"{path}.{type.Name}";

        if (IsScalar(type))
        {
            if (!before.Equals(after))
                LogDiff(before, after, fullPath);
            return;
        }
        
        IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        IEnumerable<AuditIgnoreAttribute> ignoreAttribute = type.GetCustomAttributes<AuditIgnoreAttribute>();
        
        foreach (PropertyInfo propertyInfo in properties)
        {
            AuditIgnoreAttribute? ignoreAttrib = propertyInfo.GetCustomAttribute<AuditIgnoreAttribute>();
            
            if (ignoreAttrib != null)
                continue;

            if (propertyInfo.PropertyType.FullName.Contains("Collections"))
            {
                IEnumerable enumerableA = (IEnumerable)propertyInfo.GetValue(before);
                IEnumerable enumerableB = (IEnumerable)propertyInfo.GetValue(after);

                IEnumerator enumeratorA = enumerableA.GetEnumerator();
                IEnumerator enumeratorB = enumerableB.GetEnumerator();
                

                int index = 0;
                while (enumeratorA.MoveNext() && enumeratorB.MoveNext())
                {
                    FindDiff(enumeratorA.Current, enumeratorB.Current, $"{fullPath}.{propertyInfo.Name}[{index}]", "Collection");
                    ++index;
                }
                
                continue;
            }
            
            if (!IsScalar(propertyInfo.PropertyType))
            {
                FindDiff(propertyInfo.GetValue(before), propertyInfo.GetValue(after), fullPath);
                continue;
            }
            
            if (!propertyInfo.GetValue(before).Equals(propertyInfo.GetValue(after)))
            {
                AuditNameAttribute? nameAttrib = propertyInfo.GetCustomAttribute<AuditNameAttribute>();
                
                string name = nameAttrib?.Name ?? propertyInfo.Name;
                LogDiff(propertyInfo.GetValue(before), propertyInfo.GetValue(after), $"{fullPath}.{name}");
            }
        }
    }
    
    public void Diff(object? before, object? after)
    {
        FindDiff(before, after);
    }
}