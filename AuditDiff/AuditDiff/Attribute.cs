namespace AuditDiff;

[AttributeUsage(AttributeTargets.Property)]
public class AuditNameAttribute : Attribute
{
    public string Name { get; }
    
    public AuditNameAttribute(string name)
    {
        Name = name;
    }
}

public class AuditIgnoreAttribute : Attribute
{ }