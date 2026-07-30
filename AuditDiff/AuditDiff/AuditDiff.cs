using System.Collections;
using System.Reflection;

namespace AuditDiff;

public static class AuditDiff<T>
{
    public static void Diff(T before, T after)
    {
        Type type = typeof(T);
        
        IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        IEnumerable<AuditIgnoreAttribute> ignoreAttribute = type.GetCustomAttributes<AuditIgnoreAttribute>();
        
        foreach (PropertyInfo propertyInfo in properties)
        {
            AuditIgnoreAttribute? ignoreAttrib = propertyInfo.GetCustomAttribute<AuditIgnoreAttribute>();
            
            if (ignoreAttrib != null)
                continue;

            if (propertyInfo.PropertyType.FullName.Contains("Collections"))
            {
                Console.WriteLine("Collection found");
                object? t = propertyInfo.GetValue(before);
                IEnumerable enumerator = (IEnumerable)t;
                
                if (enumerator == null)
                    continue;

                foreach (var item in enumerator)
                {
                    Console.WriteLine($"{propertyInfo.Name}.{item}");
                }
            }
         
            if (!propertyInfo.GetValue(before).Equals(propertyInfo.GetValue(after)))
            {
                AuditNameAttribute? nameAttrib = propertyInfo.GetCustomAttribute<AuditNameAttribute>();
                
                string name = nameAttrib?.Name ?? propertyInfo.Name;
                
                Console.WriteLine($"{type.Name}.{name}: {propertyInfo.GetValue(before)} | {propertyInfo.GetValue(after)}");
            }
        }
    }
}