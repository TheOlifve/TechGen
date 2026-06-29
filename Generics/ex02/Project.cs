namespace ex02;

public delegate Tout Func<Tin, Tout>(Tin item);

public class Project<Tin, Tout>
{
    private Func<Tin, Tout> _projection;
    
    public Project(Func<Tin, Tout> projection)
    {
        _projection = projection;
    }

    public Tout[] ProjectList(Tin[] items)
    {
        int cnt = 0;
        Tout[] ret = new Tout[items.Length];

        foreach (Tin item in items)
        {
            ret[cnt] = _projection.Invoke(item);
            cnt++;
        }
        
        return ret;
    }
}