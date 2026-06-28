namespace ex02;

public delegate Tout Func<Tin, Tout>(Tin item);

public class Project<Tin, Tout>
{
    Func<Tin, Tout>? Projection = null;
    
    public Project(Func<Tin, Tout> projection)
    {
        Projection = projection;
    }

    public List<Tout> ProjectList(List<Tin> items)
    {
        List<Tout> retList = new List<Tout>();
        
        foreach (var item in items)
            retList.Add(Projection.Invoke(item));
        
        return retList;
    }
}